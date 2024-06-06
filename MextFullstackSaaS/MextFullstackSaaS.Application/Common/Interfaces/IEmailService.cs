using MextFullstackSaaS.Application.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MextFullstackSaaS.Application.Common.Interfaces
{
    public interface IEmailService
    {
        Task SendEmailVerificationAsync(EmailSendEmailVerificationDto emailDto, CancellationToken cancellationToken);
        Task SendPasswordResetLinkAsync(string email, string token, CancellationToken cancellationToken);
        Task ResetPasswordAsync(string email, string token,string newPassword, CancellationToken cancellationToken);
    }
}
