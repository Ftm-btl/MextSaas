using MediatR;
using MextFullstackSaaS.Application.Common.Interfaces;
using MextFullstackSaaS.Application.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MextFullstackSaaS.Application.Features.UserAuth.Commands.ResetPassword
{
    public class UserAuthResetPasswordCommandHandler : IRequestHandler<UserAuthResetPasswordCommand, ResponseDto<bool>>
    {
        private readonly IIdentityService _identityService;

        public UserAuthResetPasswordCommandHandler(IIdentityService identityService)
        {
            _identityService = identityService;
        }
        public async Task<ResponseDto<bool>> Handle(UserAuthResetPasswordCommand request, CancellationToken cancellationToken)
        {

            var result = await _identityService.ResetPasswordAsync(request.Email,request.Token,request.NewPassword, cancellationToken);

            return new ResponseDto<bool>(result, "");
        }
    }
}
