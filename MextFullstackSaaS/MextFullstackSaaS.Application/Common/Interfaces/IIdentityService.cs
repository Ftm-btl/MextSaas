﻿using MextFullstackSaaS.Application.Common.Models;
using MextFullstackSaaS.Application.Common.Models.Auth;
using MextFullstackSaaS.Application.Features.UserAuth.Commands.login;
using MextFullstackSaaS.Application.Features.UserAuth.Commands.Password.ResetPassword;
using MextFullstackSaaS.Application.Features.UserAuth.Commands.Register;
using MextFullstackSaaS.Application.Features.UserAuth.Commands.SocialLogin;
using MextFullstackSaaS.Application.Features.UserAuth.Commands.VerifyEmail;
using MextFullstackSaaS.Application.Features.Users.Queries.GetProfile;

namespace MextFullstackSaaS.Application.Common.Interfaces
{
    public interface IIdentityService
    {
        Task<UserAuthRegisterResponseDto> RegisterAsync(UserAuthRegisterCommand command, CancellationToken cancellationToken);
        
        Task<JwtDto> LoginAsync(UserAuthLoginCommand command, CancellationToken cancellationToken);

        Task<JwtDto> SocialLoginAsync(UserAuthSocialLoginCommand command, CancellationToken cancellationToken);

        Task<bool> IsEmailExistsAsync(string email, CancellationToken cancellationToken);
        Task<bool> CheckPasswordSignInAsync(string email,string password, CancellationToken cancellationToken);

        Task<bool> VerifyEmailAsync(UserAuthVerifyEmailCommand command, CancellationToken cancellationToken);
        Task<bool> CheckIfEmailVerifiedAsync(string email, CancellationToken cancellationToken);

        Task<bool> GenerateForgetPasswordTokenAsync(string email, CancellationToken cancellationToken);
        Task<UserAuthResetPasswordResponseDto> ForgotPasswordAsync(string email, CancellationToken cancellationToken);
        Task<bool> ChangePasswordAsync(Guid userId, string currentPassword, string newPassword, CancellationToken cancellationToken);

        Task<bool> ResetPasswordAsync(UserAuthResetPasswordCommand command, CancellationToken cancellationToken);
        Task<UserGetProfileDto> GetProfileAsync(CancellationToken cancellationToken);

    }
}
