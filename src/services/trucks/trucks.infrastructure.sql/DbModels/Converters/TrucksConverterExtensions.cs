using Trucks.domain.Trucks.DbSnapshots;

namespace Trucks.Infrastructure.Sql.DbModels.Converters
{
    internal static class TrucksConverterExtensions
    {
        public static TruckDbModel ToDbModel(this TruckDbSnapshot truckDbSnapshot) =>
            new TruckDbModel
            {
                Id = truckDbSnapshot.Id,
                Code = truckDbSnapshot.Code,
                Name = truckDbSnapshot.Name,
                Description = truckDbSnapshot.Description,
                Status = truckDbSnapshot.Status
            };

        public static TruckDbSnapshot ToSnapshot(this TruckDbModel dbModel) =>
            new TruckDbSnapshot(dbModel.Id, dbModel.Code, dbModel.Name, dbModel.Description, dbModel.Status);
    }
}
