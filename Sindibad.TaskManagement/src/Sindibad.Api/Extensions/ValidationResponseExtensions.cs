using Microsoft.AspNetCore.Mvc;
using Sindibad.Api.Contracts;

namespace Sindibad.Api.Extensions;

public static class ValidationResponseExtensions
{
    public static IServiceCollection ConfigureFluentValidationResponse(
        this IServiceCollection services)
    {
        services.Configure<ApiBehaviorOptions>(options =>
        {
            options.InvalidModelStateResponseFactory = context =>
            {
                var errors = context.ModelState
                    .SelectMany(x => x.Value!.Errors)
                    .Select(x => x.ErrorMessage)
                    .ToList();

                var response = ApiResponse.Fail(
                    message: "Validation Error",
                    errors: errors);

                var loggerFactory =
                    context.HttpContext.RequestServices
                        .GetRequiredService<ILoggerFactory>();

                var logger = loggerFactory.CreateLogger("Validation");

                logger.LogWarning(
                    "Validation failed for {Path}. Errors: {@Errors}",
                    context.HttpContext.Request.Path,
                    errors);

                return new BadRequestObjectResult(response);
            };
        });

        return services;
    }
}