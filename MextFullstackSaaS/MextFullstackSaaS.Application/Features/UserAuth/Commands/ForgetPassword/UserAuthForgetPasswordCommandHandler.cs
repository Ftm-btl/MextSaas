using MediatR;
using MextFullstackSaaS.Application.Common.Interfaces;
using MextFullstackSaaS.Application.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MextFullstackSaaS.Application.Features.UserAuth.Commands.ForgetPassword
{
    public class UserAuthForgetPasswordCommandHandler : IRequestHandler<UserAuthForgetPasswordCommand, ResponseDto<bool>>
    {
        private readonly IIdentityService _identityService;

        public UserAuthForgetPasswordCommandHandler (IIdentityService identityService)
        {
            _identityService = identityService;
        }
        public async Task<ResponseDto<bool>> Handle(UserAuthForgetPasswordCommand request, CancellationToken cancellationToken)
        {
            var result= await _identityService.GenerateForgetPasswordTokenAsync(request.Email, cancellationToken);

            return new ResponseDto<bool>(result, "");
        }
    }
}
