using FluentValidation;
using Harmonix.Common.CurrentRequest;
using Harmonix.Domain.Common;

namespace Harmonix.Common;

public interface IHandler
{
}

public abstract class HandlerBase : IHandler
{
    public static Func<CurrentRequestData> GetCurrentRequest { get; set; } = () => CurrentRequestData.Empty;

    public CurrentRequestData CurrentRequest { get; set; }

    protected HandlerBase()
    {
        CurrentRequest = GetCurrentRequest();
    }
}

public abstract class BaseHandler<TResponse> : HandlerBase
{
    public abstract Task<Result<TResponse>> ExecuteAsync(CancellationToken ct);
}

public abstract class BaseHandler<TRequest, TResponse> : HandlerBase
{
    private readonly IValidator<TRequest>? _validator;

    protected BaseHandler(IValidator<TRequest>? validator = null)
    {
        _validator = validator;
    }

    public async Task<Result<TResponse>> ExecuteAsync(TRequest request, CancellationToken ct = default)
    {
        if (_validator is not null)
        {
            var validation = await _validator.ValidateAsync(request, ct);
            if (!validation.IsValid)
                return Result<TResponse>.Fail(validation.ToValidationError());
        }

        return await HandleAsync(request, ct);
    }

    protected abstract Task<Result<TResponse>> HandleAsync(TRequest request, CancellationToken ct = default);
}
