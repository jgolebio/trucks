using FluentResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trucks.Domain.Trucks;

namespace Trucks.Application.Commands;

internal class SendToJobCommandHandler : IRequestHandler<SendToJobCommand, Result>
{
    private readonly ITrucksRepository _trucksRepository;

    public SendToJobCommandHandler(ITrucksRepository trucksRepository)
    {
        _trucksRepository = trucksRepository;
    }

    public async Task<Result> Handle(SendToJobCommand request, CancellationToken cancellationToken)
    {
        var truckRes = await _trucksRepository.GetAsync(request.TruckId, cancellationToken);
        if (truckRes.IsFailed)
            return truckRes.ToResult();

        var statusChangeRes = truckRes.Value.SendToJob();
        if (statusChangeRes.IsFailed)
            return statusChangeRes.ToResult();

        _trucksRepository.Update(truckRes.Value);

        return Result.Ok();
    }
}
