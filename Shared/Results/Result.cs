using Harmonix.Shared.Errors;

namespace Harmonix.Shared.Results;

public class Result
{
    public bool IsSuccess { get; }
    public Error Error { get; }

    protected Result(bool isSuccess, Error error)
    {
        IsSuccess = isSuccess;
        Error = error ?? Error.None;
    }

    public static Result Success() => new(true, Error.None);

    public static Result Fail(Error error) => new(false, error);
}
