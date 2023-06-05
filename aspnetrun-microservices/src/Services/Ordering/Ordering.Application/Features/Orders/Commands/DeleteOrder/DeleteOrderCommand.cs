using MediatR;

namespace Ordering.Application.Features.Orders.Commands.DeleteOrder;

public class DeleteOrderCommand : IRequest<Unit>
{
    public DeleteOrderCommand(int id)
    {
        Id = id;
    }

    public int Id { get; set; }
}