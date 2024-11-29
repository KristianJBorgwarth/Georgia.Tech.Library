using FluentValidation;
using FluentValidation.Results;
using GTL.Domain.Common;
using GTL.Domain.ValueObjects;
using MediatR;

namespace GTL.Application.Behaviour
{
    public class ValidationBehavior<TRequest, TResponse>(IValidator<TRequest>? validator = null) : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
        where TResponse : Result
    {
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (validator == null)
            {
                return await next();
            }

            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (validationResult.IsValid)
            {
                return await next();
            }

            var error = GenerateErrorMessage(validationResult);

            return typeof(TResponse).IsGenericType && typeof(TResponse).GetGenericTypeDefinition() == typeof(Result<>)
                ? CreateGenericFailResponse(error)
                : CreateFailResponse(error);
        }

        private static Error GenerateErrorMessage(ValidationResult validationResult)
        {
            var errorMessage = string.Join("; ", validationResult.Errors.Select(x => x.ErrorMessage));
            return Errors.General.UnspecifiedError("Validation failed: " + errorMessage);
        }

        private static TResponse CreateFailResponse(Error error)
        {
            return (TResponse)Result.Fail(error);
        }

        private static TResponse CreateGenericFailResponse(Error error)
        {
            var resultType = typeof(TResponse).GetGenericArguments()[0];
            var method = typeof(Result).GetMethods()
                .FirstOrDefault(m => m is { Name: "Fail", IsGenericMethodDefinition: true })!;

            var genericFailMethod = method.MakeGenericMethod(resultType);
            return (TResponse)genericFailMethod.Invoke(null, [error])!;
        }
    }
}
