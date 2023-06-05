using FluentValidation;
using MediatR;

namespace Ordering.Application.Behaviours;

public class ValidationBehaviour<TRequest,TResponse> : IPipelineBehavior<TRequest,TResponse>
{
    private readonly IEnumerable<IValidator<TRequest>>? _validators;

    public ValidationBehaviour(IEnumerable<IValidator<TRequest>>? validators)
    {
        _validators = validators;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {

        if  (_validators!=null && _validators.Any())
        {
            var context = new ValidationContext<TRequest>(request);
            var validationResults =
                await Task.WhenAll(_validators.Select(validator =>
                    validator.ValidateAsync(context, cancellationToken)));
            var failures = validationResults.SelectMany(r => r.Errors).Where(f => f != null).ToList();
            if (failures.Any())
                throw new ValidationException(failures);
       
        }
        return await next();
    }
}