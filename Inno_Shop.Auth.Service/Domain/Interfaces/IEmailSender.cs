namespace Inno_Shop.Authentification.Interfaces;

public interface IEmailSender
{
    Task SendEmailAsync(string email, string subject, string message);
    Task<bool> CheckEmailExists(string email);
}