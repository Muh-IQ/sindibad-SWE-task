using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sindibad.Application.Common.Results;

public class Result
{
    public bool IsSuccess { get; private set; }
    public bool IsFailure => !IsSuccess;
    private Error _MainError;
    public Error MainError
    {
        get
        {
            if (IsSuccess)
            {
                throw new InvalidOperationException("Cannot access MainError when the result is successful.");
            }
            return _MainError;
        }

        private set
        {
            _MainError = value;
        }
    }

    public List<Error> Errors { get; private set; }

    protected Result(bool isSuccess, ErrorType errorType, string errorMessage)
    {
        IsSuccess = isSuccess;
        Errors = [];
        MainError = Error.Create(errorType, errorMessage);
    }
    protected Result(bool isSuccess)
    {
        IsSuccess = isSuccess;
    }
    public static Result Success()
        => new Result(isSuccess: true);

    public static Result Failure(ErrorType errorType, string errorMessage)
        => new Result(isSuccess: false, errorType, errorMessage);

    public Result WithError(ErrorType errorType, string errorMessage)
    {
        if (IsSuccess)
        {
            throw new InvalidOperationException("Cannot add errors to a successful result.");
        }

        Errors.Add(Error.Create(errorType, errorMessage));
        return this;
    }
}

