﻿using FluentValidation;
using System.Net;
using Patient.Application.Exceptions;

namespace Patient.Api.Middlewares;

internal class ExceptionsHandlerMiddleware(RequestDelegate next)
{
    public async Task Invoke(HttpContext context, ILogger<ExceptionsHandlerMiddleware> logger)
    {
        try
        {
            await next(context);
        }
        catch (Exception exception)
        {
            await HandleExceptionAsync(context, exception, logger);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception exception, ILogger<ExceptionsHandlerMiddleware> logger)
    {
        var code = HttpStatusCode.InternalServerError;
        var result = string.Empty;
        switch (exception)
        {
            case ValidationException validationException:
                code = HttpStatusCode.BadRequest;
                result = System.Text.Json.JsonSerializer.Serialize(validationException.Errors);
                break;
            case BadOperationException badOperationException:
                code = HttpStatusCode.BadRequest;
                result = System.Text.Json.JsonSerializer.Serialize(badOperationException.Message);
                break;
            case NotFoundException notFound:
                code = HttpStatusCode.NotFound;
                result = System.Text.Json.JsonSerializer.Serialize(notFound.Message);
                break;
        }

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)code;

        if (result == string.Empty)
            result = System.Text.Json.JsonSerializer.Serialize(new { error = exception.Message, innerMessage = exception.InnerException?.Message, exception.StackTrace });

        logger.Log(code == HttpStatusCode.InternalServerError ? LogLevel.Error : LogLevel.Warning, exception, $"Response error {code}: {exception.Message}");

        return context.Response.WriteAsync(result);
    }
}

/// <summary>
/// Extensions for using exception handling middleware
/// </summary>
public static class CoreExceptionsHandlerMiddlewareExtensions
{
    /// <summary>
    /// Adds exception handling middleware to the request processing pipeline
    /// </summary>
    /// <param name="builder">Application builder</param>
    /// <returns>Application builder with added middleware</returns>
    public static IApplicationBuilder UseExceptionHandlerMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<ExceptionsHandlerMiddleware>();
    }
}
