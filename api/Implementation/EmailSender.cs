using Application;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace Implementation
{
    public class EmailSender : IEmailSender
    {
        public void sendEmail(EmailDto email)
        {
            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential("marko.vukojevic.mailer.asp@gmail.com", "lozinkazaasp")
            };
            var message = new MailMessage("marko.vukojevic.mailer.asp@gmail.com", email.EmailTo);
            message.Subject = email.Subject;
            message.Body = email.Content;
            message.IsBodyHtml = true;
            smtp.Send(message);
        }


    }
}
