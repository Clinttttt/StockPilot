using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace StockPilot.Application.Behaviors
{
    public sealed class ValidationBehaviors<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;
        public ValidationBehaviors(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (!_validators.Any())
            {
                return await next();
            }
            var context = new ValidationContext<TRequest>(request);
            var validationResults = await Task.WhenAll(_validators.Select(s => s.ValidateAsync(context, cancellationToken)));
            var error = validationResults.SelectMany(s => s.Errors).Where(s => s != null).ToList();
            if (error.Any())
            {
               throw new ValidationException(error);
            }
            return await next();
        }
    }
}
