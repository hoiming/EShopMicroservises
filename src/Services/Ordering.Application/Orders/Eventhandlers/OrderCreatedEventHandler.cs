


namespace Ordering.Application.Orders.Eventhandlers
{
    public class OrderCreatedEventHandler(ILogger<OrderCreatedEventHandler> logger) : INotificationHandler<OrderUpdatedEvent>
    {
        public Task Handle(OrderUpdatedEvent notification, CancellationToken cancellationToken)
        {
            logger.LogInformation("Domain informatin logged");
            return Task.CompletedTask;
        }
    }
}
