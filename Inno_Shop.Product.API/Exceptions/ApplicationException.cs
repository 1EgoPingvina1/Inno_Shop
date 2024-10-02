namespace Inno_Shop.Product.API.Exceptions;

public class ApplicationException : Exception
{
    public string Title { get; }

    public ApplicationException(string title, string message) : base(message)
    {
        Title = title;
    }
}