using System.Configuration;
using System.Net;
using System.Net.Mail;

namespace Aidify
{
    public static class EmailService
    {
        public static void Send(string to, string subject, string htmlBody)
        {
            var smtpUser = ConfigurationManager.AppSettings["SmtpUser"];
            var smtpPass = ConfigurationManager.AppSettings["SmtpPass"];

            using (var client = new SmtpClient("smtp.gmail.com", 587))
            {
                client.Credentials = new NetworkCredential(smtpUser, smtpPass);
                client.EnableSsl = true;

                var msg = new MailMessage(smtpUser, to, subject, htmlBody)
                {
                    IsBodyHtml = true
                };

                client.Send(msg);
            }
        }
    }
}
