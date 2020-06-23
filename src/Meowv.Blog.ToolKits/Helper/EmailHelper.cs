﻿using System.Linq;

using MimeKit;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using Meowv.Blog.Domain.Configurations;

namespace Meowv.Blog.ToolKits.Helper
{
    public static class EmailHelper
    {
        public static async Task SendEmailAsync(MimeMessage message)
        {
            if (!message.From.Any())
            {
                message.From.Add(new MailboxAddress(AppSettings.Email.From.Name, AppSettings.Email.From.Address));
            }
            if (!message.To.Any())
            {
                var address = AppSettings.Email.To.Select(x => new MailboxAddress(x.Key, x.Value));
                message.To.AddRange(address);
            }

            using var client = new SmtpClient
            {
                ServerCertificateValidationCallback = (s, c, h, e) => true
            };
            client.AuthenticationMechanisms.Remove("XOAUTH2");

            await client.ConnectAsync(AppSettings.Email.Host, AppSettings.Email.Port, AppSettings.Email.UseSsl);
            await client.AuthenticateAsync(AppSettings.Email.From.Username, AppSettings.Email.From.Password);
            await client.SendAsync(message);
            await client.DisconnectAsync(true);
        }
    }
}