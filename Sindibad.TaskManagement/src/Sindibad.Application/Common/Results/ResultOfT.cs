using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sindibad.Application.Common.Results;

public class Result<T> : Result
{
    public T? Value { get; private set; }

    protected Result(bool isSuccess, T? value, ErrorType errorType, string errorMessage)
        : base(isSuccess, errorType, errorMessage)
    {
        Value = value;
    }
    protected Result(bool isSuccess, T? value)
        : base(isSuccess)
    {
        Value = value;
    }

    public static Result<T> Success(T value)
        => new Result<T>(true, value);

    public static new Result<T> Failure(ErrorType errorType, string errorMessage)
        => new Result<T>(false, default, errorType, errorMessage);

    public new Result<T> WithError(ErrorType errorType, string errorMessage)
    {
        base.WithError(errorType, errorMessage);
        return this;
    }
}