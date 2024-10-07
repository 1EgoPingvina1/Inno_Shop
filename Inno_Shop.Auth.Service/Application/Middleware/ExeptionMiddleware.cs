using System.Text.Json;
using FluentValidation;
using Inno_Shop.Authentification.Application.Exceptions;
using ApplicationException = Inno_Shop.Authentification.Application.Exceptions.ApplicationException;

namespace Inno_Shop.Authentification.Application.Middleware;

internal sealed class ExceptionMiddleware : IMiddleware
{
    private readonly ILogger<ExceptionMiddleware> _logger;
    public ExceptionMiddleware(ILogger<ExceptionMiddleware> logger) => _logger = logger;
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            await HandellExceptionHadlingMiddleware(context, ex);
        }
    }
    
    private static async Task HandellExceptionHadlingMiddleware(HttpContext httpContext, Exception exception)
    {
        var statusCode = GetStatusCode(exception);

        var response = new
        {
            title = GetTitle(exception),
            status = statusCode,
            detail = exception.Message,
            errors = GetErrors(exception)
        };
        
        httpContext.Response.ContentType = "application/json";
        
        httpContext.Response.StatusCode = statusCode;
        
        await httpContext.Response.WriteAsync(JsonSerializer.Serialize(response));
        
    }
    
    private static string GetTitle(Exception exception) =>
        exception switch
        {
            BadRequestException badRequestException => "BadRequest",
            UnauthorizedAccessException unauthorizedAccessException => "Unauthorized",
            NotFoundException notFoundException => "NotFound",
            InnoValidationExeption validationException => "Invalid login or password",
            ApplicationException applicationException => applicationException.Title,
            _ => "Server Error"
        };

    private static int GetStatusCode(Exception exception) =>
        exception switch
        {
            BadRequestException => StatusCodes.Status400BadRequest,
            UnauthorizedAccessException => StatusCodes.Status401Unauthorized,
            NotFoundException=> StatusCodes.Status404NotFound,
            InnoValidationExeption => StatusCodes.Status422UnprocessableEntity,
            _ => StatusCodes.Status500InternalServerError
        };
    
    private static InvalidModelProperty[]? GetErrors(Exception exception)
    {
        InvalidModelProperty[] errors = null!;
        if(exception is InnoValidationExeption validationException)
            errors = validationException.ErrorsDictionary;
        return errors;
    }
}