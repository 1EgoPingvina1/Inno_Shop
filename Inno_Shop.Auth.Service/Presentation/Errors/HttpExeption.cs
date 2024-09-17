namespace Inno_Shop.Authentification.API.Errors;

public class HttpExeption : Exception
{
    public int StatusCode { get; private set; }

    public HttpExeption(int statusCode, string message) : base($"{statusCode} - {message}")
    {
        StatusCode = statusCode;
    }

}