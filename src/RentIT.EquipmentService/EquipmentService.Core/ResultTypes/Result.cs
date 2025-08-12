using System.Diagnostics.CodeAnalysis;
namespace EquipmentService.Core.ResultTypes;

public class Result
{
    public bool IsSuccess { get; }
    public Error Error { get; }

    public bool IsFailure
    {
        get => !IsSuccess;
    }


    protected Result(bool isSuccess, Error error)
    {
        if (isSuccess && error != Error.None ||
            !isSuccess && error == Error.None)
        {
            throw new ArgumentException("Invalid error", nameof(error));
        }

        IsSuccess = isSuccess;
        Error = error;
    }


    public static Result Success()
    {
        return new Result(true, Error.None);
    }
    public static Result Failure(Error error)
    {
        return new Result(false, error);
    }
    public static Result<TValue> Success<TValue>(TValue value)
    {
        return new Result<TValue>(value, true, Error.None);
    }
    public static Result<TValue> Failure<TValue>(Error error)
    {
        return new Result<TValue>(default, false, error);
    }

}

public class Result<TValue> : Result
{

    private readonly TValue? _value;
    public Result(TValue? value, bool isSuccess, Error error)
        : base(isSuccess, error)
    {
        _value = value;
    }

    [NotNull]
    public TValue Value => IsSuccess ? _value! : throw new InvalidOperationException("The value of a failure can't be accessed");

    public static implicit operator Result<TValue>(TValue? value) =>
        value is not null ? Success(value) : Failure<TValue>(Error.NullValue);

}

