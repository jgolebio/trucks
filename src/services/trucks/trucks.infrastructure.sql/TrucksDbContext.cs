using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Collections.Concurrent;
using System.Data;
using Trucks.Domain.SeedWork;
using Trucks.Infrastructure.Sql.DbModels;
using Trucks.Infrastructure.Sql.EntityConfigurations;

namespace Trucks.Infrastructure.Sql;

public class TrucksDbContext : DbContext, IUnitOfWork
{
    public DbSet<TruckDbModel> Trucks { get; set; }

    private IDbContextTransaction _currentTransaction = null!;
    public bool HasActiveTransaction => _currentTransaction is not null;
    private readonly ConcurrentDictionary<Guid, List<IDomainEventsHolder>> _changedAggregates;
    private readonly IMediator _mediator;

    public TrucksDbContext(DbContextOptions<TrucksDbContext> options, IMediator mediator) : base(options)
    {
        _changedAggregates = new();
        _mediator = mediator;
    }

    public void AddToChangedAggregates(IDomainEventsHolder aggregate) =>
        _changedAggregates[_currentTransaction.TransactionId].Add(aggregate);


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("trc");
        modelBuilder.ApplyConfiguration(new TrucksConfiguration());
    }

    public async Task<IDbContextTransaction> BeginTransactionAsync()
    {
        if (_currentTransaction != null) return null!;

        _currentTransaction = await Database.BeginTransactionAsync(IsolationLevel.ReadCommitted);
        var res = _changedAggregates.TryAdd(_currentTransaction.TransactionId, new());
        if (!res)
            throw new InvalidOperationException($"Invalid state. Transaction already exists {_currentTransaction.TransactionId}");

        return _currentTransaction;
    }

    public async Task CommitTransactionAsync(IDbContextTransaction transaction)
    {
        if (transaction == null) throw new ArgumentNullException(nameof(transaction));
        if (transaction != _currentTransaction) throw new InvalidOperationException($"Transaction {transaction.TransactionId} is not current");

        try
        {
            await DispatchDomainEventsAsync();

            await SaveChangesAsync();
            await transaction.CommitAsync();
        }
        catch
        {
            RollbackTransaction();
            throw;
        }
        finally
        {
            if (_currentTransaction != null)
            {
                _changedAggregates.TryRemove(_currentTransaction.TransactionId, out _);
                _currentTransaction.Dispose();
                _currentTransaction = null!;
            }
        }
    }

    public void RollbackTransaction()
    {
        try
        {
            _currentTransaction?.Rollback();
        }
        finally
        {
            if (_currentTransaction != null)
            {
                _changedAggregates.TryRemove(_currentTransaction.TransactionId, out _);
                _currentTransaction.Dispose();
                _currentTransaction = null!;
            }
        }
    }

    private async Task DispatchDomainEventsAsync()
    {
        var changedAggregate = _changedAggregates[_currentTransaction.TransactionId];
        var domainEvents = changedAggregate.SelectMany(x => x.DomainEvents).ToList();
        changedAggregate.ForEach(x => x.ClearDomainEvents());

        foreach (var domainEvent in domainEvents)
            await _mediator.Publish(domainEvent);
    }

    public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default)
    {
        _ = await base.SaveChangesAsync(cancellationToken);

        return true;
    }
}
