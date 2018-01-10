using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Core.Notifications
{
    public class SendNotification
    {
        public bool SendMailAccountVerification(string toAddress, string name, int userId, string codeString, string subject)
        {
            try
            {
                var url = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) + HttpContext.Current.Request.ApplicationPath;
                var finalurl = string.Format("{0}Home/ActivateUser?id={1}&code={2}", url, userId, codeString);
                string body = createEmailBody(name, "Request for Verification", finalurl);

                var status = SendEmail(toAddress, subject, body);

                return status;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool SendMailForgotPassword(string toAddress, string name, string password, string subject)
        {
            try
            {
                string body = "Hello " + toAddress + ",";
                body += "<br /><br />Below is the password for your account";
                body += "<br />" + password;
                body += "<br /><br />Thanks";

                SendEmail(toAddress, subject, body);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public string createEmailBody(string userName, string title, string Url)
        {
            string body = string.Empty;
            //using streamreader for reading my htmltemplate   

            using (StreamReader reader = new StreamReader(HttpContext.Current.Server.MapPath("~/HtmlTemplates/AccountVerification.html")))
            {
                body = reader.ReadToEnd();
            }
            body = body.Replace("{UserName}", userName); //replacing the required things  
            body = body.Replace("{Title}", title);
            body = body.Replace("{Url}", Url);

            return body;
        }

        public bool SendEmail(string toAddress, string subject, string body)
        {
            try
            {
                MailMessage mm = new MailMessage();
                mm.To.Add(new MailAddress(toAddress, subject));
                mm.From = new MailAddress("manjinder.impinge@gmail.com");

                mm.Body = body;
                mm.IsBodyHtml = true;
                mm.Subject = subject;
                SmtpClient smcl = new SmtpClient();
                smcl.Send(mm);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
