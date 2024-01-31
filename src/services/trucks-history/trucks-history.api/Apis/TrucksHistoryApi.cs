using Microsoft.AspNetCore.Http.HttpResults;
using TrucksHistory.Application.Queries;


namespace TrucksHistory.API.Apis;

public static class TrucksHistoryApi
{
    public static RouteGroupBuilder MapTrucksHistoryApi(this RouteGroupBuilder app)
    {
        app.MapGet("/", GetTrucksAsync);

        return app;
    }

    private static async Task<Results<Ok<GetTrucksQuery.TrucksViewModel>, BadRequest<string>>> GetTrucksAsync([AsParameters] TrucksHistoryService service)
    {
        var query = new GetTrucksQuery();
        var result = await service.Mediator.Send(query);
        if (result.IsFailed)
            return TypedResults.BadRequest<string>(result.Errors.First().Message);

        return TypedResults.Ok<GetTrucksQuery.TrucksViewModel>(result.Value);
    }
}
