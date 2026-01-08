using FluentValidation;
using Harmonix.Shared.Extensions;
using Harmonix.Shared.Results;

namespace Harmonix.Shared.Application;

public abstract class BaseService<TRequest, TResponse>
{
    private readonly IValidator<TRequest>? _validator;

    protected BaseService(IValidator<TRequest>? validator = null)
    {
        _validator = validator;
    }
    public async Task<Result<TResponse>> ExecuteAsync(TRequest request, CancellationToken ct)
    {
        if (_validator is not null)
        {
            var validation = _validator.Validate(request);
            if (!validation.IsValid)
                return Result<TResponse>.Fail(validation.ToValidationError());
        }

        return await HandleAsync(request, ct);
    }

    protected abstract Task<Result<TResponse>> HandleAsync(TRequest request, CancellationToken ct);
}
