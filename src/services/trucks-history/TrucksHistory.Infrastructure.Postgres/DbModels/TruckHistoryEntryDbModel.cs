namespace TrucksHistory.Infrastructure.Postgres.DbModels;

public class TruckHistoryEntryDbModel
{
    public Guid Id { get; set; }
    public DateTime EntryDate { get; set; }
    public string Status { get; set; } = null!;
    public int StatusCode { get; set; }
    public Guid TruckId { get; set; }

    //navigations
    public TruckHistoryDbModel TruckHistory { get; set; } = null!;
}
