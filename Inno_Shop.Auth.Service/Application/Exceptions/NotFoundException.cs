namespace Inno_Shop.Authentification.Application.Exceptions;

public class NotFoundException : Exception
{
    public NotFoundException(string message, string errorCode) : base(message)
    {
        ErrorCode = errorCode;
    }

    public string ErrorCode { get; set; }
}