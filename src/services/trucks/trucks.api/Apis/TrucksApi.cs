using Microsoft.AspNetCore.Http.HttpResults;
using Trucks.application.Commands;

namespace Trucks.api.Apis
{
    public static class TrucksApi
    {
        public static RouteGroupBuilder MapTrucksApi(this RouteGroupBuilder app)
        {
            app.MapPost("/", CreateTruckAsync);

            return app;
        }

        private static async Task<Results<Ok<CreateTruckCommand.CreateTruckResult>, BadRequest<string>>> CreateTruckAsync(
        CreateTruckCommand.CreateTruckRequest request,
        [AsParameters] TrucksService service)
        {
            var command = new CreateTruckCommand(request);
            var result = await service.Mediator.Send(command);
            if (result.IsFailed)
                return TypedResults.BadRequest<string>(result.Errors.First().Message);

            return TypedResults.Ok<CreateTruckCommand.CreateTruckResult>(result.Value);
        }
    }
}
