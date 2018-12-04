using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using MadCapTest.Logic.Abstractions;

namespace MadCapTest.Logic
{
    public class NotificationService : INotificationService
    {
        // TODO: THIS WILL NOT WORK, SMPT PARAMETERS ARE PLACEHOLDERS
        public void SendMessage(string sender, string subject, string body, List<string> recipients)
        {
            var client = new SmtpClient("HOST")
            {
                Credentials = new NetworkCredential("USER", "XXX"),
                UseDefaultCredentials = false
            };

            var from = new MailAddress(sender);

            var message = new MailMessage
            {
                From = from,
                Subject = subject,
                Body = body
            };

            message.To.Add(string.Join(",", recipients));

            client.Send(message);
        }
    }
}