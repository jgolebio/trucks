namespace Trucks.Application.Queries;

public class TruckQuery
{
    public string? Name { get; set; }
    public string? Code { get; set; }
    public string? OrderBy { get; set; }
    public bool? IsAcending { get; set; }
    public int? Page {  get; set; }
    public int? PageSize { get; set;}
    public int? Status { get; set; }
}
