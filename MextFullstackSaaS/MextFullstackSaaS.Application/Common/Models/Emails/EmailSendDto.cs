using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MextFullstackSaaS.Application.Common.Models.Emails
{
    public class EmailSendDto
    {
        public string Subject { get; set; }
        public List<string> Addresses { get; set; }
        public string HtmlContent { get; set; }

        public EmailSendDto(string email,string subject,string htmlBody) 
        {
            Subject=subject;
            HtmlContent=htmlBody;
            Addresses = new() { email };
        }
        public EmailSendDto(List<string> emails, string subject, string htmlBody)
        {
            Subject = subject;

            Addresses = emails;

            HtmlContent = htmlBody;
        }


    }
}
