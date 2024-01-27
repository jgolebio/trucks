using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using trucks.application.Commands;
using trucks.application.Queries;
using Trucks.application.Commands;

namespace Trucks.api.Apis;

public static class TrucksApi
{
    public static RouteGroupBuilder MapTrucksApi(this RouteGroupBuilder app)
    {
        app.MapPost("/", CreateTruckAsync);
        app.MapPut("/{truckId}", UpdateTruckAsync);
        app.MapDelete("/{truckId}", DeleteTruckAsync);
        app.MapPut("/{truckId}/StartLoading", StartLoadingAsync);
        app.MapPut("/{truckId}/SendToJobn", SendToJobAsync);
        app.MapPut("/{truckId}/NotifyAtJob", NotifyAtJobAsync);
        app.MapPut("/{truckId}/Return", ReturnAsync);
        app.MapPut("/{truckId}/OutOfService", OutOfServiceAsync);
        app.MapGet("/", GetTrucksAsync);

        return app;
    }

    private static async Task<Results<Ok, BadRequest<string>>> ReturnAsync([FromRoute] Guid truckId,
        [AsParameters] TrucksService service)
    {
        var command = new ReturnCommand(truckId);
        var result = await service.Mediator.Send(command);
        if (result.IsFailed)
            return TypedResults.BadRequest<string>(result.Errors.First().Message);

        return TypedResults.Ok();
    }

    private static async Task<Results<Ok, BadRequest<string>>> NotifyAtJobAsync([FromRoute] Guid truckId,
        [AsParameters] TrucksService service)
    {
        var command = new NotifyAtJobCommand(truckId);
        var result = await service.Mediator.Send(command);
        if (result.IsFailed)
            return TypedResults.BadRequest<string>(result.Errors.First().Message);

        return TypedResults.Ok();
    }

    private static async Task<Results<Ok, BadRequest<string>>> StartLoadingAsync([FromRoute] Guid truckId,
        [AsParameters] TrucksService service)
    {
        var command = new StartLoadingCommand(truckId);
        var result = await service.Mediator.Send(command);
        if (result.IsFailed)
            return TypedResults.BadRequest<string>(result.Errors.First().Message);

        return TypedResults.Ok();
    }

    private static async Task<Results<Ok, BadRequest<string>>> SendToJobAsync([FromRoute] Guid truckId,
        [AsParameters] TrucksService service)
    {
        var command = new SendToJobCommand(truckId);
        var result = await service.Mediator.Send(command);
        if (result.IsFailed)
            return TypedResults.BadRequest<string>(result.Errors.First().Message);

        return TypedResults.Ok();
    }

    private static async Task<Results<Ok, BadRequest<string>>> OutOfServiceAsync([FromRoute] Guid truckId,
        [AsParameters] TrucksService service)
    {
        var command = new OutOfServiceCommand(truckId);
        var result = await service.Mediator.Send(command);
        if (result.IsFailed)
            return TypedResults.BadRequest<string>(result.Errors.First().Message);

        return TypedResults.Ok();
    }

    private static async Task<Results<Ok, BadRequest<string>>> DeleteTruckAsync([FromRoute] Guid truckId,
        [AsParameters] TrucksService service)
    {
        var command = new DeleteTruckCommand(truckId);
        var result = await service.Mediator.Send(command);
        if (result.IsFailed)
            return TypedResults.BadRequest<string>(result.Errors.First().Message);

        return TypedResults.Ok();
    }

    private static async Task<Results<Ok, BadRequest<string>>> UpdateTruckAsync([FromRoute] Guid truckId,
        [FromBody] UpdateTruckCommand.UpdtaeTruckRequest request, [AsParameters] TrucksService service)
    {
        var command = new UpdateTruckCommand(truckId, request);
        var result = await service.Mediator.Send(command);
        if (result.IsFailed)
            return TypedResults.BadRequest<string>(result.Errors.First().Message);

        return TypedResults.Ok();
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

    private static async Task<Results<Ok<GetTrucksQuery.TrucksViewModel>, BadRequest<string>>> GetTrucksAsync(
        string? name, string? code,int? status, int? page, int? pageSize, string? orderBy, bool? IsAscending,
        [AsParameters] TrucksService service)
    {
        var truckQuery = new TruckQuery
        {
            Code = code,
            Name = name,
            Status = status,
            OrderBy = orderBy,
            IsAcending = IsAscending,
            Page = page,
            PageSize = pageSize,
        };

        var query = new GetTrucksQuery(truckQuery);
        var result = await service.Mediator.Send(query);
        if (result.IsFailed)
            return TypedResults.BadRequest<string>(result.Errors.First().Message);

        return TypedResults.Ok<GetTrucksQuery.TrucksViewModel>(result.Value);
    }
}
