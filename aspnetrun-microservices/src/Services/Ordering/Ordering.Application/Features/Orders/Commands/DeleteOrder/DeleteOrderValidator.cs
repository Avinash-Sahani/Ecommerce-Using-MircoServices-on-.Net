using FluentValidation;

namespace Ordering.Application.Features.Orders.Commands.DeleteOrder;

public class DeleteOrderValidator:AbstractValidator<DeleteOrderCommand>
{
    public DeleteOrderValidator()
    {
        RuleFor(command => command.Id).NotNull().NotEmpty().WithMessage("ID is required").NotNull();

    }
}