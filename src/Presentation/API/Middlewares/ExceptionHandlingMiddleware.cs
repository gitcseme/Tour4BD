using Microsoft.AspNetCore.Mvc;
using SharedKarnel.Contracts;
using SharedKarnel.Exceptions;

namespace API.Middlewares;

public class ExceptionHandlingMiddleware
{
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;
    private readonly RequestDelegate _next;

    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "Exception occurred: {Message}", exception.Message);

            if (exception is CommandValidationException cve)
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                await context.Response.WriteAsJsonAsync(
                    Result<IDictionary<string, string[]>>.Failure(cve.Errors, "Validation failure"));

                return;
            }

            if (exception is InvalidRequestException invalidException)
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                await context
                    .Response
                    .WriteAsJsonAsync(Result<object>.Failure(new { message = $"{invalidException.Error}" },
                     "Invalid request data"));

                return;
            }

            var problemDetails = new ProblemDetails
            {
                Status = StatusCodes.Status500InternalServerError,
                Title = "Server Error",
                Type = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.6.1",
                // comment out this property while in production
                Detail = exception.InnerException?.Message ?? exception.Message
            };

            context.Response.StatusCode = StatusCodes.Status500InternalServerError;

            await context.Response.WriteAsJsonAsync(
                Result<ProblemDetails>.Failure(problemDetails, "Server Error"));
        }
    }
}
