using MediatR;
using MextFullstackSaaS.Application.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MextFullstackSaaS.Application.Features.UserAuth.Commands.ForgetPassword
{
    public class UserAuthForgetPasswordCommand : IRequest<ResponseDto<bool>>
    {
        public string Email { get; set; }
        public UserAuthForgetPasswordCommand(string email)
        {
            Email = email;
        } 
        public UserAuthForgetPasswordCommand()
        {
          
        }

    }
}
