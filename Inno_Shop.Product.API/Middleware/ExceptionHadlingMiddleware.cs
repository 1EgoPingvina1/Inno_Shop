using System.Text.Json;
using Inno_Shop.Product.API.Exceptions;
using ApplicationException = Inno_Shop.Product.API.Exceptions.ApplicationException;
using ValidationException = System.ComponentModel.DataAnnotations.ValidationException;

namespace Inno_Shop.Product.API.Middleware;

public class ExceptionHadlingMiddleware : IMiddleware
{
    private readonly ILogger<ExceptionHadlingMiddleware> _logger;

    public ExceptionHadlingMiddleware(ILogger<ExceptionHadlingMiddleware> logger)
    {
        _logger = logger;
    }
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
            ApplicationException applicationException => applicationException.Title,
            _ => "Server Error"
        };

    private static int GetStatusCode(Exception exception) =>
        exception switch
        {
            BadRequestException => StatusCodes.Status400BadRequest,
            NotFoundException=> StatusCodes.Status404NotFound,
            ValidationException => StatusCodes.Status422UnprocessableEntity,
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