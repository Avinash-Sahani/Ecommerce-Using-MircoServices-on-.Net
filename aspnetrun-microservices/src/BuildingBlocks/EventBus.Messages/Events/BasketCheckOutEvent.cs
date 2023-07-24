#nullable enable
using Ordering.Domain.Entities;

namespace EventBus.Messages.Events;

public class BasketCheckOutEvent : IntegrationBaseEvent
{
    public Order Order { get; set; } 

    public BasketCheckOutEvent()
    {
        Order = new Order();
    }

    public BasketCheckOutEvent(Order order)
    {
        this.Order = order;
    }

}