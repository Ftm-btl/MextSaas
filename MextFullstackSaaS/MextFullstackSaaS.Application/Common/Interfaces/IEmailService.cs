using MextFullstackSaaS.Application.Common.Models.Email;
using MextFullstackSaaS.Application.Common.Models.Emails;
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
        Task SendEmailResetPasswordAsync(EmailSendResetPasswordDto emailDto, CancellationToken cancellationToken);
        Task SendPasswordChangedNotificationAsync(string email, CancellationToken cancellationToken);
    }
}
