using BookstoreEmailService.Models;
using MailKit.Net.Smtp;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookstoreEmailService.Services
{
    public class EmailService : IEmailService
    {
        private readonly EmailConfig _emailConfig;
        public EmailService(EmailConfig emailConfig) => _emailConfig = emailConfig;

        public void SendEmail(MailMessage message)
        {
            var emailMessage = CreateEmailMessage(message);
            Send(emailMessage);
        }

        private MimeMessage CreateEmailMessage(MailMessage message)
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress("email",_emailConfig.From));
            emailMessage.To.AddRange(message.To);
            emailMessage.Subject = message.Subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Text) { Text = message.Content };
            
            return emailMessage;
        }

        private void Send(MimeMessage mailMessage)
        {
            using var client = new SmtpClient();
            try
            {
                client.Connect(_emailConfig.SmtpServer, _emailConfig.Port, true);
                client.AuthenticationMechanisms.Remove("XOAUTH2");
                client.Authenticate(_emailConfig.UserName, _emailConfig.Password);
                
                client.Send(mailMessage);
            }
            catch (Exception ex)
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
