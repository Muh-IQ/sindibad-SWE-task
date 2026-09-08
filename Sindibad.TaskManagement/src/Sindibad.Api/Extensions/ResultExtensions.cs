using Microsoft.AspNetCore.Mvc;
using Sindibad.Application.Common.Results;

namespace Sindibad.Api.Extensions;

public static class ResultExtensions
{
    public static IActionResult ToHttpResult(this Result result)
    {
        if (result.IsSuccess)
        {
            return new OkObjectResult(
                new
                {
                    success = true
                });
        }

        return new ObjectResult(
            new
            {
                success = false,
                message = result.MainError.Message,
                errors = result.Errors.Select(error => new
                {
                    type = error.ErrorType.ToString(),
                    message = error.Message
                })
            })
        {
            StatusCode = (int)result.MainError.ErrorType
        };
    }

    //this is overload to choose a specific success code
    public static IActionResult ToHttpResult(this Result result, int successStatusCode)
    {
        if (result.IsSuccess)
        {
            return new ObjectResult(new
            {
                success = true
            })
            {
                StatusCode = successStatusCode
            };
        }

        return result.ToHttpResult();
    }

    public static IActionResult ToHttpResult<T>(this Result<T> result)
    {
        if (result.IsSuccess)
        {
            return new OkObjectResult(
                new
                {
                    success = true,
                    data = result.Value
                });
        }

        return new ObjectResult(
            new
            {
                success = false,
                message = result.MainError.Message,
                errors = result.Errors.Select(error => new
                {
                    type = error.ErrorType.ToString(),
                    message = error.Message
                })
            })
        {
            StatusCode = (int)result.MainError.ErrorType
        };
    }

    //this is overload to choose a specific success code
    public static IActionResult ToHttpResult<T>(this Result<T> result, int successStatusCode)
    {
        if (result.IsSuccess)
        {
            return new ObjectResult(new
            {
                success = true,
                data = result.Value
            })
            {
                StatusCode = successStatusCode
            };
        }

        return result.ToHttpResult();
    }
}
