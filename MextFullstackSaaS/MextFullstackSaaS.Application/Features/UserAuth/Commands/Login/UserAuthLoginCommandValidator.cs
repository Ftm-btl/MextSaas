using FluentValidation;
using MextFullstackSaaS.Application.Common.FluentValidator.BaseValidators;
using MextFullstackSaaS.Application.Common.Interfaces;
using MextFullstackSaaS.Application.Features.UserAuth.Commands.login;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MextFullstackSaaS.Application.Features.UserAuth.Commands.Login
{
    public class UserAuthLoginCommandValidator : UserAuthValidatorBase<UserAuthLoginCommand>
    {
        public UserAuthLoginCommandValidator(IIdentityService identityService) : base(identityService)
        {   RuleFor(x => x.Email)
             .NotEmpty().WithMessage("Email is required")
             .EmailAddress().WithMessage("Email is not valid");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required")
                .MinimumLength(6).WithMessage("Password must be at least 6 characters");

            RuleFor(x => x.Email)
                .MustAsync((x, y, cancellationToken) => CheckPasswordSignInAsync(x.Email, x.Password, cancellationToken))
                .WithMessage("Your Email or Password is incorrect. Please try again.");

            RuleFor(x => x.Email)
                .MustAsync(CheckIfEmailVerifiedAsync)
                .WithMessage("Email is not verified. please verify your email.");

        }
        private Task<bool> CheckIfEmailVerifiedAsync(string email, CancellationToken cancellationToken)
        {
            return _identityService.CheckIfEmailVerifiedAsync(email, cancellationToken);
        }
        private Task<bool> CheckPasswordSignInAsync(string email, string password, CancellationToken cancellationToken)
        {
            return _identityService.CheckPasswordSignInAsync(email, password, cancellationToken);
        }


    }
}
