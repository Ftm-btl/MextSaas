using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MextFullstackSaaS.Application.Common.Models.Emails
{
    public class SendEmailVerificationDto
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string Token { get; set; }

        public SendEmailVerificationDto(string? email, string firstName, string token)
        {
            Email = email; ;
            FirstName = firstName;
            Token = token;
        }

        public SendEmailVerificationDto() { }
    }
}
