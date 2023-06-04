using FluentValidation;

namespace Ordering.Application.Features.Orders.Commands.CheckOutOrder;

public class CheckOutCommandValidator : AbstractValidator<CheckOutOrderCommand>
{
    public CheckOutCommandValidator()
    {
        RuleFor(command => command.Order.UserName).NotEmpty().WithMessage("UserName is required").NotNull()
            .MaximumLength(50).WithMessage(command => $"{command.Order.UserName} must not exceed 50 characters");
        
        RuleFor(command => command.Order.EmailAddress).NotEmpty().WithMessage("Email is required").NotNull();

        RuleFor(command => command.Order.TotalPrice).NotEmpty().WithMessage("UserName is required").NotNull()
            .GreaterThan(0).WithMessage(command => "{ command.Order.TotalPrice} should be greater than zero");
    }
}