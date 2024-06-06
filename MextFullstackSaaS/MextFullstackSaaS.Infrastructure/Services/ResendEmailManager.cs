using MextFullstackSaaS.Application.Common.Interfaces;
using MextFullstackSaaS.Application.Common.Models;
using Resend;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using static System.Net.WebRequestMethods;

namespace MextFullstackSaaS.Infrastructure.Services
{
    public class ResendEmailManager : IEmailService
    {
        private readonly IResend _resend;

        public ResendEmailManager(IResend resend)
        {
            _resend = resend;
        }

        private const string ApiBaseUrl = "https://localhost:7281/api/";
        public Task SendEmailVerificationAsync(EmailSendEmailVerificationDto emailDto, CancellationToken cancellationToken)
        {
            var encodedEmail=HttpUtility.UrlEncode(emailDto.Email);

            var encodedToken=HttpUtility.UrlEncode(emailDto.Token);

            var link = $"{ApiBaseUrl}UsersAuth/verify-email?email={encodedEmail}&token={encodedToken}";

            var message = new EmailMessage();
            message.From = "onboarding@resend.dev";
            message.To.Add(emailDto.Email);
            message.Subject = "Email Verification | IconBuilderAI!";
            message.HtmlBody = $"<div><a href=\"{link}\" target=\"_blank\"><strong>Greetings<strong> 👋🏻 from .NET</a></div>";

            return _resend.EmailSendAsync(message,cancellationToken);
        }

        public  Task SendPasswordResetLinkAsync(string email, string token, CancellationToken cancellationToken)
        {
            var encodedEmail = HttpUtility.UrlEncode(email);

            var encodedToken = HttpUtility.UrlEncode(token);

            var link = $"{ApiBaseUrl}UserAuth/forget-password?email={encodedEmail}&token={encodedToken}&newPassword=";

            var message = new EmailMessage();
            message.From = "onboarding@resend.dev";
            message.To.Add(email);
            message.Subject = "Email Verification | IconBuilderAI!";
            message.HtmlBody = $"<div><a href=\"{link}\" target=\"_blank\"><strong>Greetings<strong> 👋🏻 from .NET</a></div>";
            
            
            return _resend.EmailSendAsync(message,cancellationToken);
        }

        public Task ResetPasswordAsync(string email, string token, string newPassword, CancellationToken cancellationToken)
        {

            return Task.FromResult(true);
        }
    }
}
