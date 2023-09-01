namespace OrderManagement.Application.Common.Behaviours;

public sealed class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse> where TResponse : Response
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var context = new ValidationContext<TRequest>(request);

        var failures = _validators.Select(v => v.Validate(context))
                                    .SelectMany(r => r.Errors.Where(e => !string.IsNullOrEmpty(e.ErrorMessage)).Select(e => e.ErrorMessage))
                                    .Where(x => x != null)
                                    .ToList();

        if (failures.Any())
        {
            var responseType = typeof(TResponse);

            if (responseType.IsGenericType)
            {
                var resultType = responseType.GetGenericArguments()[0];
                var invalidResponseType = typeof(Response<>).MakeGenericType(resultType);
                var invalidResponse = Activator.CreateInstance(invalidResponseType) as TResponse;
                var addErrorsMethod = invalidResponseType.GetMethod("AddErrorMessages");
                object[] parametersArray = new object[] { failures };
                addErrorsMethod.Invoke(invalidResponse, parametersArray);

                return invalidResponse;
            }
        }

        return await next();
    }
}