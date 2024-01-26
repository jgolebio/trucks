namespace Trucks.Infrastructure.Sql.DbModels;

public class TruckDbModel
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string Code { get; set; } = null!;
    public int Status { get; set; }
}
