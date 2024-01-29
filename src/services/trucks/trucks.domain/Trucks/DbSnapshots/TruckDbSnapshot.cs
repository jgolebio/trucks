namespace Trucks.Domain.Trucks.DbSnapshots;

public record TruckDbSnapshot(Guid Id, string Code, string Name, string Description, int Status);
