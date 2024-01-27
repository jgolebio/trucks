﻿using FluentResults;
using MediatR;
using trucks.application.Behaviors;

namespace trucks.application.Commands;

public class StartLoadingCommand : BaseCommand, IRequest<Result>
{
    public Guid TruckId { get; }

    public StartLoadingCommand(Guid truckId)
    {
        TruckId = truckId;
    }
}
