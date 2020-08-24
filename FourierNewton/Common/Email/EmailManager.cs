using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Mail;
using System.Text;
using System.Web;
using FourierNewton.Common.Cryptography;

namespace FourierNewton.Common.Email
{
    public class EmailManager
    {

        public static void SendEmail(string emailAddressTo) {


            MailMessage mail = new MailMessage();
            mail.From = new MailAddress(EmailConstants.EmailAddressFrom, EmailConstants.DispLayName, Encoding.UTF8);
            mail.To.Add(emailAddressTo);
            mail.Subject = EmailConstants.EmailSubject;
            mail.SubjectEncoding = Encoding.UTF8;
            mail.Body = generateEmailBody(emailAddressTo);
            mail.BodyEncoding = Encoding.UTF8;
            mail.IsBodyHtml = true;
            //mail.Priority = MailPriority.High;
            SmtpClient client = new SmtpClient();
            client.Credentials = new System.Net.NetworkCredential(EmailConstants.EmailAddressFrom, EmailConstants.EmailAddressFromPassword);
            client.Port = EmailConstants.SmtpClientPort;
            client.Host = EmailConstants.SmtpClientHost;
            client.EnableSsl = true;
            try
            {
                client.Send(mail);

            }
            catch (SmtpException smtpException)
            {
                var errorMessage = smtpException.Message;
            }

        }

        private static string generateEmailBody(string emailAddressTo) {

            string password = CryptographyManager.GeneratePassword();

            string emailBody = "<h2>Welcome,</h2><br>" +

                               "<h3>&nbsp;&nbsp;&nbsp;&nbsp;Username: " + emailAddressTo + "</h3><br>" +

                               "<h3>&nbsp;&nbsp;&nbsp;&nbsp;Password: " + password + "</h3><br>" +

                               "<h2>Thank you</h2><br>" +

                               "<a href =" + ProjectConstants.BaseUrl + ">" + ProjectConstants.BaseUrl + "</a>";

            return emailBody;

        }

    }
}
