 using FluentResults;
using Microsoft.EntityFrameworkCore;
using trucks.domain.SeedWork;
using Trucks.domain.Trucks;
using Trucks.Infrastructure.Sql.DbModels.Converters;

namespace Trucks.Infrastructure.Sql.Repositories
{
    public class TrucksRepository : ITrucksRepository
    {
        private readonly TrucksDbContext _dbContext;

        public TrucksRepository(TrucksDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IUnitOfWork UnitOfWork => _dbContext;

        public void Add(Truck model)
        {
            _dbContext.Trucks.Add(model.ToDbSnapshot().ToDbModel());
        }

        public void Delete(Truck model)
        {
            _dbContext.Trucks.Remove(model.ToDbSnapshot().ToDbModel());
        }

        public async Task<Result<Truck>> GetAsync(Guid truckId, CancellationToken cancellationToken)
        {
            try
            {
                var dbModel = await _dbContext.Trucks.FirstOrDefaultAsync(x => x.Id == truckId, cancellationToken);
                if (dbModel is null)
                    return Result.Fail("Not found");

                return Truck.CreateFromDbSnapshot(dbModel.ToSnapshot());
            }
            catch (Exception ex)
            {
                return Result.Fail(ex.Message);
            }
        }

        public void Update(Truck model)
        {
            _dbContext.Trucks.Update(model.ToDbSnapshot().ToDbModel());
        }
    }
}
