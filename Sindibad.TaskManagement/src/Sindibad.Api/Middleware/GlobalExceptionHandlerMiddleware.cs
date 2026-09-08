using Sindibad.Api.Contracts;
using Sindibad.Api.Middleware.ExceptionHandling;

namespace Sindibad.Api.Middleware;

/// <summary>
/// Global exception handler. Catches unhandled system/infrastructure exceptions,
/// logs them, and returns a consistent ApiResponse JSON response.
/// </summary>
public class GlobalExceptionHandlerMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<GlobalExceptionHandlerMiddleware> _logger;
    private readonly IWebHostEnvironment _env;

    public GlobalExceptionHandlerMiddleware(
        RequestDelegate next,
        ILogger<GlobalExceptionHandlerMiddleware> logger,
        IWebHostEnvironment env)
    {
        _next = next;
        _logger = logger;
        _env = env;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private async Task HandleExceptionAsync(
        HttpContext context,
        Exception exception)
    {
        if (context.Response.HasStarted)
        {
            _logger.LogError(
                exception,
                "Response already started; cannot write error response. Rethrowing.");

        }

        var (statusCode, message) = ExceptionMapping.Map(exception);

        if (_env.IsDevelopment() &&
            statusCode >= StatusCodes.Status500InternalServerError &&
            !string.IsNullOrWhiteSpace(exception.Message))
        {
            message = exception.Message;
        }

        _logger.LogError(
            exception,
            "Unhandled exception. StatusCode: {StatusCode}",
            statusCode);

        context.Response.StatusCode = statusCode;
        context.Response.ContentType = "application/json";

        var response = ApiResponse.Fail(message);

        await context.Response.WriteAsJsonAsync(response);
    }
}