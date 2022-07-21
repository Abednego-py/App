
using MailKit;
using MimeKit;
//using App.Services;
using Microsoft.Extensions.Options;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Identity.UI.Services;
using App.Services;

public class EmailSender : IEmailSender
{
    private readonly EmailConfiguration _emailConfig;

    private readonly ILogger _logger;


    public EmailSender(EmailConfiguration emailConfig, ILogger<EmailSender> logger)
    {
        _emailConfig = emailConfig;
        _logger = logger;
    }



    public async Task SendEmailAsync(string email, string subject, string htmlMessage)
    {
        var emailMessage = new MimeMessage();
        emailMessage.From.Add(new MailboxAddress(String.Empty, _emailConfig.From));
        emailMessage.To.Add(new MailboxAddress(String.Empty,email));
        emailMessage.Subject = subject;
        emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Text) { Text = htmlMessage};

        using (var client = new SmtpClient())
        {
            try
            {
                client.Connect(_emailConfig.SmtpServer, _emailConfig.Port, true);
                client.AuthenticationMechanisms.Remove("XOAUTH2");
                client.Authenticate(_emailConfig.UserName, _emailConfig.Password);

                await client.SendAsync(emailMessage);

                //_logger.LogInformation(response);
            }
            catch
            {
                //log an error message or throw an exception or both.
                throw;
            }
            finally
            {
                client.Disconnect(true);
                client.Dispose();
            }
        }
    }

    
  }

