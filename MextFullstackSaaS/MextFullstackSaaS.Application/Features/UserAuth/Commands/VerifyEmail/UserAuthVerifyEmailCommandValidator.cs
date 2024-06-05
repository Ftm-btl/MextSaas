using FluentValidation;
using MextFullstackSaaS.Application.Common.FluentValidator.BaseValidators;
using MextFullstackSaaS.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MextFullstackSaaS.Application.Features.UserAuth.Commands.VerifyEmail
{
    public class UserAuthVerifyEmailCommandValidator : UserAuthValidatorBase<UserAuthVerifyEmailCommand>
    {
        
        public UserAuthVerifyEmailCommandValidator(IIdentityService identityService):base(identityService)
        {

            RuleFor(p => p.Email)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .EmailAddress().WithMessage("{PropertyName} is not a valid email address.");

            RuleFor(p => p.Token)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .MinimumLength(10)
                .WithMessage("{PropertyName} must be at least 10 characters long.");

            RuleFor(p => p.Email)
                .MustAsync(CheckIfUserExists)
                .WithMessage("User with this email does not exist");
        }

        private Task<bool> CheckIfUserExists(string email, CancellationToken cancellationToken)
        {
            return _identityService.IsEmailExistsAsync(email, cancellationToken);
        }
    }
}
