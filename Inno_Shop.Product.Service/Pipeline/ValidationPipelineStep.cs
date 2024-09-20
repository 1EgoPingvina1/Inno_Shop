using FluentValidation;
using MediatR;

namespace Inno_Shop.Product.Service.Pipeline;

public class ValidationPipelineStep<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidationPipelineStep(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (_validators.Any())
        {
            var context = new ValidationContext<TRequest>(request);
            var results = await Task.WhenAll(_validators.Select(p => p.ValidateAsync(context, cancellationToken)));
            var failures = results.SelectMany(r => r.Errors).Where(f => f != null).ToList();

            if (failures.Any())
            {
                //var props = failures.Select(x => new InvalidModelProperty(x.PropertyName, x.ErrorMessage));
                //throw new ScoprValidationException(props.ToArray());
            }
        }

        return await next();
    }
}