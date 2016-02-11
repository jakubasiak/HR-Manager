using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;
using System.Net;

namespace HR_Manager.Models
{
    public class EmailSender : IEmailSender
    {
        public bool SendEmail(ContactViewModel model, ApplicationUser user, string emailReceiver)
        {
            try
            {
                MailAddress from = new MailAddress(model.ReturnAddress, user.FirstName + " " + user.Surname, System.Text.Encoding.UTF8);
                MailAddress to = new MailAddress(emailReceiver);

                MailMessage message = new MailMessage(from, to);
                message.Subject = model.MessageSubject;
                message.Body = model.Message;
                message.BodyEncoding = System.Text.Encoding.UTF8;
                message.SubjectEncoding = System.Text.Encoding.UTF8;
                message.IsBodyHtml = true;


                SmtpClient client = new SmtpClient("smtp.gmail.com");
                client.Port = 587;
                client.EnableSsl = true;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential("username", "password");

                client.Send(message);
                message.Dispose();

                return true;

            }
            catch(Exception e)
            {
                return false;
            }
        }
    }
}