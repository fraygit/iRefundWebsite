using iRefundUmbraco7.Custom.Data.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace iRefundUmbraco7.Custom.Service
{
    public class MailService
    {
        public void Send(string to, string templatePath, string username, RefundApplication refundApplication, string smtpServer, string smtpUsername, string smtpPassword, string fromEmail)
        {
            MailMessage mail = new MailMessage();
            SmtpClient client = new SmtpClient();
            client.Port = 25;
            client.EnableSsl = true;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            client.Host = smtpServer;

            //mail.To.Add(new MailAddress(to));
            
            mail.To.Add(new MailAddress(to));
            mail.From = new MailAddress(fromEmail);
            mail.Subject = "iRefund Application";
            mail.IsBodyHtml = true;
            var param = new string[] { refundApplication.IRDNumber, refundApplication.Title, refundApplication.FirstName, refundApplication.MiddleName, refundApplication.LastName, refundApplication.DateOfBirth, refundApplication.Email, refundApplication.DayTimePhone, refundApplication.MobilePhone, refundApplication.HowYouHeard, refundApplication.HaveNZDriverLicense, refundApplication.DriverLicenseNumber, refundApplication.CardVersion };
            mail.Body = GetMessage(param, templatePath);
            client.UseDefaultCredentials = true;
            client.Credentials = new NetworkCredential(smtpUsername, smtpPassword);
            var userToken = "something";
            client.Send(mail);
        }

        private string GetMessage(string[] parameters, string templatePath)
        {
            if (File.Exists(templatePath))
            {
                var template = File.ReadAllText(templatePath);
                for (var i = 0; i < parameters.Length; i++)
                {
                    template = template.Replace("{" + i + "}", parameters[i]);
                }
                return template;
            }
            return "";
        }
    }
}
