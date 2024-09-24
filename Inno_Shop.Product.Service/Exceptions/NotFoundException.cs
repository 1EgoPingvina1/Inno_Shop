namespace Inno_Shop.Product.Service.Exceptions;

public class NotFoundException : Exception
{

    public NotFoundException(string message, string errorCode) : base(message)
    {
        ErrorCode = errorCode;
    }

    public string ErrorCode { get; set; }
}