using FluentResults;
using Microsoft.EntityFrameworkCore;
using trucks.domain.SeedWork;
using Trucks.domain.Trucks;
using Trucks.domain.Trucks.DbSnapshots;
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

        public void Add(TruckDbSnapshot dbSnapshot)
        {
            _dbContext.Trucks.Add(dbSnapshot.ToDbModel());
        }

        public async Task<Result<TruckDbSnapshot>> GetAsync(Guid truckId)
        {
            try
            {
                var dbModel = await _dbContext.Trucks.FirstOrDefaultAsync(x => x.Id == truckId);
                if (dbModel is null)
                    return Result.Fail("Not found");

                return dbModel.ToSnapshot();
            }
            catch (Exception ex)
            {
                return Result.Fail(ex.Message);
            }
        }
    }
}
