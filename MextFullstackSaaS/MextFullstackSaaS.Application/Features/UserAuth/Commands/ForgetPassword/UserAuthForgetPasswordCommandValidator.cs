using FluentValidation;
using MextFullstackSaaS.Application.Common.FluentValidator.BaseValidators;
using MextFullstackSaaS.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MextFullstackSaaS.Application.Features.UserAuth.Commands.ForgetPassword
{
    public class UserAuthForgetPasswordCommandValidator : UserAuthValidatorBase<UserAuthForgetPasswordCommand>
    {     

        public UserAuthForgetPasswordCommandValidator(IIdentityService identityService): base(identityService)
        {

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required")
                .EmailAddress().WithMessage("Email is not valid")
                .MustAsync(CheckIfEmailExistsAsync).WithMessage("Email does not exist");
        }
        private Task<bool> CheckIfEmailExistsAsync(string email, CancellationToken cancellationToken)
        {
            return _identityService.IsEmailExistsAsync(email, cancellationToken);
        }
    }
}
