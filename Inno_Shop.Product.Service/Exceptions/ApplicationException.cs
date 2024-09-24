namespace Inno_Shop.Product.Service.Exceptions;

public class ApplicationException : Exception
{
    public string Title { get; }

    public ApplicationException(string title, string message) : base(message)
    {
        Title = title;
    }
}