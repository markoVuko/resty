using System;
using System.Collections.Generic;
using System.Text;

namespace Application
{
    public interface IEmailSender
    {
        void sendEmail(EmailDto email);
    }
    public class EmailDto
    {
        public string EmailTo { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }
    }
}
