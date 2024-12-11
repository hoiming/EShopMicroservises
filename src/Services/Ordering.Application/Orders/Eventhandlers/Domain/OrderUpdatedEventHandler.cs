

using MediatR;
using Microsoft.Extensions.Logging;
using Ordering.Domain.Events;

namespace Ordering.Application.Orders.Eventhandlers.Domain
{
    public class OrderUpdatedEventHandler(ILogger<OrderUpdatedEventHandler> logger) : INotificationHandler<OrderCreatedEvent>
    {
        public Task Handle(OrderCreatedEvent notification, CancellationToken cancellationToken)
        {
            logger.LogInformation("Lomain Event handled: {DomainEvent}", notification.GetType().Name);
            return Task.CompletedTask;
        }
    }
}
