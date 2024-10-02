namespace Inno_Shop.Product.API.Exceptions;

public class InvalidModelProperty
{
    public string PropertyName { get; set; }
    public string ErrorMessage { get; set; }

    public InvalidModelProperty(string propertyName, string errorMessage)
    {
        PropertyName = propertyName;
        ErrorMessage = errorMessage;
    }
}