using FluentValidation;
using MextFullstackSaaS.Application.Common.FluentValidator.BaseValidators;
using MextFullstackSaaS.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MextFullstackSaaS.Application.Features.UserAuth.Commands.ResetPassword
{
    public class UserAuthResetPasswordCommandValidator: UserAuthValidatorBase<UserAuthResetPasswordCommand>
    {
        public UserAuthResetPasswordCommandValidator(IIdentityService identityService) : base(identityService) 
        {
            RuleFor(p => p.Email)
                .NotEmpty().WithMessage("Email is required")
                .EmailAddress().WithMessage("Email is not valid"); 

            RuleFor(p => p.Token)
                .NotEmpty().WithMessage("Email is required")
                .EmailAddress().WithMessage("Email is not valid");

            RuleFor(p => p.NewPassword)
                .NotEmpty().WithMessage("Email is required")
                .EmailAddress().WithMessage("Email is not valid");
        }
        private Task<bool> CheckIfEmailExistsAsync(string email, CancellationToken cancellationToken)
        {
            return _identityService.IsEmailExistsAsync(email, cancellationToken);
        }
    }
}
