using System.ComponentModel.DataAnnotations;

namespace Inno_Shop.Authentification.Application.Exceptions;

public class InnoValidationExeption : ValidationException
{
    public InnoValidationExeption(InvalidModelProperty[] errors)
    {
        ErrorsDictionary = errors;
    }

    public InvalidModelProperty[] ErrorsDictionary { get; set; }
}