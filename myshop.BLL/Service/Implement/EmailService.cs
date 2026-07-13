using Microsoft.Extensions.Options;
using MimeKit;
using myshop.BLL.Service.Abstract;
using myshop.BLL.ViewModels;
using System;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;

using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myshop.BLL.Service.Implement
{
    public class EmailService : IEmailService
    {
        private readonly EmailSettings settings;
        public EmailService(IOptions<EmailSettings> options)
        {
            settings = options.Value;
        }
        public async Task SendEmailAsync(string to, string subject, string body)
        {
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(settings.Email));
            email.To.Add(MailboxAddress.Parse(to));
            email.Subject = subject;
            email.Body = new TextPart("html")
            {
                Text = body
            };
            using var smtp = new SmtpClient();
            await smtp.ConnectAsync(settings.Host, settings.Port, MailKit.Security.SecureSocketOptions.StartTls);
            Console.WriteLine($"Email: {settings.Email}");
            Console.WriteLine($"Password: {settings.Password}");

            await smtp.AuthenticateAsync(settings.Email, settings.Password);
            await smtp.SendAsync(email);
            await smtp.DisconnectAsync(true);
        }
    }

    }
