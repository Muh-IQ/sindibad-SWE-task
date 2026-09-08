using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sindibad.Application.Common.Results;

public class Error
{

    public ErrorType ErrorType { get; private set; }


    public string Message { get; private set; } = null!;

    private Error()
    {

    }
    private Error(ErrorType errorType, string message)
    {

        ErrorType = errorType;
        Message = message;
    }


    public static Error Create(ErrorType errorType, string message) => new Error(errorType, message);
}