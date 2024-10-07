namespace Inno_Shop.Authentification.Application.Exceptions;

public class BadRequestException : Exception
{
    public BadRequestException(string message, string errorCode) : base(message)
    {
        ErrorCode = errorCode;
    }

    public string ErrorCode { get; set; }
}