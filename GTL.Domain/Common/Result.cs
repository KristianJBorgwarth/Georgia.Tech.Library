using GTL.Domain.ValueObjects;

namespace GTL.Domain.Common;

public class Result
{
    public bool Success { get; private set; }
    public Error Error { get; private set; }

    public bool Failure => !Success;

    protected Result(bool success, Error error)
    {
        Success = success;
        Error = error;
    }

    public static Result Fail(Error error)
    {
        return new Result(false, error);
    }

    public static Result<T> Fail<T>(Error error)
    {
        return new Result<T>(default, false, error);
    }

    public static Result Ok()
    {
        return new Result(true, null);
    }

    public static Result<T> Ok<T>(T? value = default)
    {
        return new Result<T>(value, true, null);
    }
    

    public static Result Combine(params Result[] results)
    {
        foreach (var result in results)
        {
            if (result.Failure)
                return result;
        }

        return Ok();
    }

}


public class Result<T> : Result
{
    private readonly T? _value;

    public T? Value
    {
        get
        {
            if (!Success) throw new InvalidOperationException("Cannot fetch value on a failed result");

            return _value;
        }

        private init => _value = value;
    }

    protected internal Result(T? value, bool success, Error error)
        : base(success, error)
    {
        Value = value;
    }

    public static implicit operator Result<T>(T from)
    {
        return Ok(from);
    }

    public static implicit operator T(Result<T> from)
    {
        return from.Value;
    }
}