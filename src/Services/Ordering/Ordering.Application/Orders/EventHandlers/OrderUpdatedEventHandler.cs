﻿using MediatR;
using Microsoft.Extensions.Logging;
using Ordering.Domain.Events;

namespace Ordering.Application.Orders.EventHandlers;

public class OrderUpdatedEventHandler : INotificationHandler<OrderUpdatedEvent>
{
    private readonly ILogger<OrderUpdatedEventHandler> _logger;

    public OrderUpdatedEventHandler(ILogger<OrderUpdatedEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(OrderUpdatedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Domain event handled: {domainEvent}" + notification.GetType());
        return Task.CompletedTask;
    }
}