namespace TrucksHistory.Infrastructure.Postgres.DbModels;
public class TruckHistoryDbModel
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string Code { get; set; } = null!;
    public DateTime CreateDate { get; set; }
    public string Status { get; set; } = null!;
    public int StatusCode { get; set; }
}
