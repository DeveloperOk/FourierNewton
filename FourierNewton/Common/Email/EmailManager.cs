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

        private static void SendEmail(string emailAddressTo, string emailSubject, string emailBody) {


            MailMessage mail = new MailMessage();
            mail.From = new MailAddress(EmailConstants.EmailAddressFrom, EmailConstants.DispLayName, Encoding.UTF8);
            mail.To.Add(emailAddressTo);
            mail.Subject = emailSubject;
            mail.SubjectEncoding = Encoding.UTF8;
            mail.Body = emailBody;
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

        public static void SendEmailForAccountGeneration(string emailAddressTo, string password)
        {

            string emailBody = generateEmailBodyForAccountGeneration(emailAddressTo, password);
            SendEmail(emailAddressTo, EmailConstants.EmailSubjectForAccountGeneration, emailBody);

        }


        private static string generateEmailBodyForAccountGeneration(string emailAddressTo, string password) {

            string emailBody = "<h2>Welcome,</h2><br>" +

                               "<h3>&nbsp;&nbsp;&nbsp;&nbsp;Username: " + emailAddressTo + "</h3><br>" +

                               "<h3>&nbsp;&nbsp;&nbsp;&nbsp;Password: " + password + "</h3><br>" +

                               "<h2>Thank you</h2><br>" +

                               "<a href =" + ProjectConstants.BaseUrl + ">" + ProjectConstants.BaseUrl + "</a>";

            return emailBody;

        }

        public static void SendEmailForResettingPassword(string emailAddressTo, string password)
        {

            string emailBody = generateEmailBodyForResettingPassword(emailAddressTo, password);
            SendEmail(emailAddressTo, EmailConstants.EmailSubjectForResettingPassword, emailBody);

        }

        private static string generateEmailBodyForResettingPassword(string emailAddressTo, string password)
        {

            string emailBody = "<h2>Password is reset,</h2><br>" +

                               "<h3>&nbsp;&nbsp;&nbsp;&nbsp;Username: " + emailAddressTo + "</h3><br>" +

                               "<h3>&nbsp;&nbsp;&nbsp;&nbsp;Password: " + password + "</h3><br>" +

                               "<h2>Thank you</h2><br>" +

                               "<a href =" + ProjectConstants.BaseUrl + ">" + ProjectConstants.BaseUrl + "</a>";

            return emailBody;

        }



    }
}
