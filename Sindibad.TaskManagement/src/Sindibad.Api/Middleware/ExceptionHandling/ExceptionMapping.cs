namespace Sindibad.Api.Middleware.ExceptionHandling;

public static class ExceptionMapping
{
    public static (int StatusCode, string Message) Map(Exception exception)
    {
        return (
            StatusCodes.Status500InternalServerError,
            "An unexpected error occurred. Please try again later"
        );
    }
}