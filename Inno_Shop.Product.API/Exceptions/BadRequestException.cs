namespace Inno_Shop.Product.API.Exceptions;

public class BadRequestException : Exception
{
    public BadRequestException(string message, string errorCode) : base(message)
    {
        ErrorCode = errorCode;
    }

    public string ErrorCode { get; set; }
}