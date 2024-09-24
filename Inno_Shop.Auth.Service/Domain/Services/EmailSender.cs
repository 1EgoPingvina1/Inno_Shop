using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Azure.Core;
using Inno_Shop.Authentification.Domain.Interfaces;
using Inno_Shop.Authentification.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace Inno_Shop.Authentification.Domain.Services;

public class EmailSender : IEmailSender
{
    private readonly ILogger<EmailSender> _logger;
    private readonly string? _smtpServer;
    private readonly int _smtpPort;
    private readonly string? _smtpUsername;
    private readonly string? _smtpPassword;
    private readonly UserManager<User> _userManager;

    public EmailSender(ILogger<EmailSender> logger, IConfiguration configuration, UserManager<User> userManager)
    {
        _logger = logger;
        _userManager = userManager;
        _smtpServer = configuration["Email:SmtpServer"];
        _smtpPort = int.Parse(configuration["Email:SmtpPort"]);
        _smtpUsername = configuration["Email:SmtpUsername"];
        _smtpPassword = configuration["Email:SmtpPassword"];
    }

    public async Task SendEmailAsync(string email, string subject, string message)
    {
        try
        {
            using var client = new SmtpClient();
            client.Host = _smtpServer;
            client.Port = _smtpPort;
            client.EnableSsl = true;
            client.Credentials = new NetworkCredential(_smtpUsername, _smtpPassword);

            using var mailMessage = new MailMessage();
            mailMessage.To.Add(email);
            mailMessage.Subject = subject;
            mailMessage.Body = message;
            mailMessage.IsBodyHtml = true;

            await client.SendMailAsync(mailMessage);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error sending email");
        }
    }

    public async Task SendTokenAsync(User user, string token)
    {
        var callbackUrl = $"{user.Email}?token={token}";
        var subject = "Email Confirmation";
        var message = $"<p>Please confirm your email by clicking this link: <a href='{callbackUrl}'>{callbackUrl}</a></p>";

        await SendEmailAsync(user.Email, subject, message);
    }

    public async Task<bool> CheckEmailExists(string email)
        => await _userManager.FindByEmailAsync(email) != null;
}