using MediatR;
using MextFullstackSaaS.Application.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MextFullstackSaaS.Application.Features.UserAuth.Commands.ResetPassword
{
    public class UserAuthResetPasswordCommand:IRequest<ResponseDto<bool>>
    {
        public string Email { get; set; }
        public string Token { get; set; }
        public string NewPassword { get; set; }

        public UserAuthResetPasswordCommand(string email, string token, string newPassword)
        {
            Email = email;
            Token = token;
            NewPassword = newPassword;
        }
    }
}
