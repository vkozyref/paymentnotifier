using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace PaymentNotifier.Smtp
{
    public class Emailer
    {
        public void Send(NotificationSettings settings, Notifications notification)
        {
            var fromAddress = new MailAddress(ConfigurationManager.AppSettings["email"]);
            var toAddress = new MailAddress(settings.Addressee);
            string fromPassword = ConfigurationManager.AppSettings["password"];
            string subject = settings.Subject;
            string body = string.Format(settings.Pattern, notification.SentOn, notification.Value);

            var smtp = BuildSmtpClent(fromAddress.Address, fromPassword);
            var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = body
            };

            ServicePointManager.ServerCertificateValidationCallback = delegate (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };

            using (smtp)
            using (message)
            {
                smtp.Send(message);
            }
        }

        private SmtpClient BuildSmtpClent(string address, string password)
        {
            return new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(address, password)
            };
        }
    }
}
