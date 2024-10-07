using FluentValidation;
using Inno_Shop.Authentification.Application.Exceptions;
using MediatR;
using ValidationException = System.ComponentModel.DataAnnotations.ValidationException;

namespace Inno_Shop.Authentification.Infrastructure.FluentPipline;

public sealed class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;
    public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
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
                var props = failures.Select(x => new InvalidModelProperty(x.PropertyName, x.ErrorMessage));
                throw new InnoValidationExeption(props.ToArray());
            }
        }

        return await next();
    }
}