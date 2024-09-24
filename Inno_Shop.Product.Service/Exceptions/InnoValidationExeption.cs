using System.ComponentModel.DataAnnotations;

namespace Inno_Shop.Product.Service.Exceptions;

public class InnoValidationExeption : ValidationException
{
    public InnoValidationExeption(InvalidModelProperty[] errors)
    {
        ErrorsDictionary = errors;
    }

    public InvalidModelProperty[] ErrorsDictionary { get; }
}