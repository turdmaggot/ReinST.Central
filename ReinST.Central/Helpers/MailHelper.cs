using System;
using System.Configuration;
using System.Net;
using System.Net.Mail;

namespace ReinST.Central.Helpers
{
    public static class MailHelper
    {
        /// <summary>
        /// Sends an email. Note: You have to set the proper AppSettings variable
        /// in order for this to work properly.
        /// </summary>
        /// <param name="content">
        /// Content to scan for a default image.
        /// </param>
        /// <param name="defaultImageURL">
        /// URL to of the image to display when no image tags are found.
        /// </param>
        public static void Sendmail(string to, string from, string subject, string body)
        {
            try
            {
                string host = ConfigurationManager.AppSettings["HOST"];
                int port = int.Parse(ConfigurationManager.AppSettings["PORT"]);
                bool UseDefaultCreds = bool.Parse(ConfigurationManager.AppSettings["USE_DEFAULT_CREDENTIALS"]);
                bool EnableSSL = bool.Parse(ConfigurationManager.AppSettings["ENABLE_SSL"]);
                string username = ConfigurationManager.AppSettings["USERNAME"];
                string password = ConfigurationManager.AppSettings["PASSWORD"];

                MailMessage message = new MailMessage();
                message.To.Add(to);
                message.Subject = subject;
                message.From = new MailAddress(from);
                message.Body = body;
                SmtpClient smtp = new SmtpClient(host, port);
                smtp.UseDefaultCredentials = UseDefaultCreds;
                smtp.EnableSsl = EnableSSL;
                smtp.Credentials = new NetworkCredential(username, password);
                //smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.Send(message);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
