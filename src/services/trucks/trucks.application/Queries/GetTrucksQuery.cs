using FluentResults;
using MediatR;

namespace trucks.application.Queries;

public class GetTrucksQuery : IRequest<Result<GetTrucksQuery.TrucksViewModel>>
{
    public TruckQuery Payload { get; }

    public GetTrucksQuery(TruckQuery query)
    {
        Payload = query;
    }

    public record TruckViewModel(Guid Id, string Code, string Name, string Description, int StatusCode, string Status);
    public record TrucksViewModel(int ItemsNumber, TruckViewModel[] Items);
}
