using MimeKit;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using System;

namespace PawFect.Services
{
    public class MailService
    {
        private readonly IConfiguration _configuration;

        public MailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public bool SendEmail(string toEmail, string subject, string body, string fromName = "PawFect", string fromEmail = "edakaracoban1997@gmail.com")
        {
            try
            {
                var smtpSettings = _configuration.GetSection("SmtpSettings");

                MimeMessage mimeMessage = new MimeMessage();
                mimeMessage.From.Add(new MailboxAddress(fromName, fromEmail));
                mimeMessage.To.Add(new MailboxAddress(toEmail, toEmail));
                mimeMessage.Subject = subject;

                var bodyBuilder = new BodyBuilder();
                bodyBuilder.HtmlBody = body;

                mimeMessage.Body = bodyBuilder.ToMessageBody();

                using (var client = new SmtpClient())
                {
                    client.Connect(smtpSettings["Host"], int.Parse(smtpSettings["Port"]), false);
                    client.Authenticate(smtpSettings["Username"], smtpSettings["Password"]);
                    client.Send(mimeMessage);
                    client.Disconnect(true);
                }

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Mail gönderim hatası: " + ex.Message);
                return false;
            }
        }
    }
}
