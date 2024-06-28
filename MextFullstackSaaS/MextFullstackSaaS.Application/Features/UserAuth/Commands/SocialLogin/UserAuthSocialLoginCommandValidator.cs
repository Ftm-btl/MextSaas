using FluentValidation;
using MextFullstackSaaS.Application.Common.FluentValidator.BaseValidators;
using MextFullstackSaaS.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MextFullstackSaaS.Application.Features.UserAuth.Commands.SocialLogin
{
    public class UserAuthSocialLoginCommandValidator : UserAuthValidatorBase<UserAuthSocialLoginCommand>
    {
       
        public UserAuthSocialLoginCommandValidator(IIdentityService identityService) : base(identityService)
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required")
                .Must(IsEmail).WithMessage("Email is not valid");
            //.EmailAddress().WithMessage("Email is not valid"); 

            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("First name is required");

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("Last name is required");

        }
    }
}
