using FluentResults;
using MediatR;
using static TrucksHistory.Application.Queries.GetTrucksQuery;

namespace TrucksHistory.Application.Queries;
public class GetTrucksQuery : IRequest<Result<TrucksViewModel>>
{
    public record class TruckViewModel(Guid Id, string Code, string Name, string Status);
    public record class TrucksViewModel(TruckViewModel[] Items, int Count);
}
