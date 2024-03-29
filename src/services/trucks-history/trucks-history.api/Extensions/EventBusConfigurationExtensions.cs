﻿using EventBus.Abstractions;
using TrucksHistory.Application.IntegrationEvents.EventHandlers;
using TrucksHistory.Application.IntegrationEvents.Events;

namespace TrucksHistory.API.Extensions;

public static class EventBusConfigurationExtensions
{
    public static void ConfigureEventBus(this IApplicationBuilder app)
    {
        var eventBus = app.ApplicationServices.GetRequiredService<IEventBus>();
        eventBus.Subscribe<TruckCreatedIntegrationEvent, TruckCreatedIntegrationEventHandler>();
        eventBus.Subscribe<TruckUpdateIntegrationEvent, TruckUpdateIntegrationEventHandler>();
        eventBus.Subscribe<TruckStatusChangedIntegrationEvent, TruckStatusChangedIntegrationEventHandler>();
    }
}
