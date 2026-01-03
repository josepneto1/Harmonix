using Harmonix.Shared.Errors;

namespace Harmonix.Shared.Results;

public sealed class Result<T> : Result
{
    public T? Data { get; }

    private Result(bool isSuccess, T? data, Error error)
        : base(isSuccess, error)
    {
        Data = data;
    }

    public static Result<T> Success(T data) => new(true, data, Error.None);
    public static new Result<T> Fail(Error error) => new(false, default, error);
}
