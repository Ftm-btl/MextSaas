﻿using MextFullstackSaaS.Application.Common.Interfaces;
using MextFullstackSaaS.Application.Common.Models;
using MextFullstackSaaS.Application.Features.UserAuth.Commands.Register;
using MextFullstackSaaS.Domain.Identity;
using Microsoft.AspNetCore.Identity;
using MextFullstackSaaS.Application.Common.Models.Auth;
using MextFullstackSaaS.Application.Features.UserAuth.Commands.login;
using Microsoft.EntityFrameworkCore;
using MextFullstackSaaS.Application.Features.UserAuth.Commands.VerifyEmail;
using System.Net;
using System.Web;
using MextFullstackSaaS.Application.Features.UserAuth.Commands.SocialLogin;
using MextFullstackSaaS.Domain.Entities;
using MextFullstackSaaS.Application.Features.UserAuth.Commands.Password.ResetPassword;
using MextFullstackSaaS.Application.Features.Users.Queries.GetProfile;

namespace MextFullstackSaaS.Infrastructure.Services
{
    public class IdentityManager : IIdentityService
    {
        private readonly IJwtService _jwtService;
        private readonly UserManager<User> _userManager;
        private readonly IEmailService _emailService;
        private readonly ICurrentUserService _currentUserService;
        private readonly IApplicationDbContext _applicationDbContext;

        public IdentityManager(UserManager<User> userManager, IJwtService jwtService, IEmailService emailService,ICurrentUserService currentUserService,IApplicationDbContext applicationDbContext)
        {
            _userManager = userManager;
            _jwtService = jwtService;
            _emailService = emailService;
            _currentUserService = currentUserService;
            _applicationDbContext = applicationDbContext;

        }
        public async Task<UserAuthRegisterResponseDto> RegisterAsync(UserAuthRegisterCommand command, CancellationToken cancellationToken)
        {
            var user = UserAuthRegisterCommand.ToUser(command);

            var result = await _userManager.CreateAsync(user, command.Password);

            if (!result.Succeeded)
            {
                throw new Exception("User registration failed");
            }

            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

            return new UserAuthRegisterResponseDto(user.Id, user.Email, user.FirstName, token);

        }

        public async Task<JwtDto> LoginAsync(UserAuthLoginCommand command, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(command.Email);

            var jwtDto = await _jwtService.GenerateTokenAsync(user.Id, user.Email, cancellationToken);

            return jwtDto;
        }

        public async Task<bool> IsEmailExistsAsync(string email, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user is not null)
                return true;

            return false;
        }

        public async Task<bool> CheckPasswordSignInAsync(string email, string password, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user is null) return false;

            return await _userManager.CheckPasswordAsync(user, password);
        }

        public async Task<bool> VerifyEmailAsync(UserAuthVerifyEmailCommand command, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(command.Email);

            var result = await _userManager.ConfirmEmailAsync(user, command.Token);

            if (!result.Succeeded)
            {
                throw new Exception("User's email verification failed");
            }

            return true;
        }

        public Task<bool> CheckIfEmailVerifiedAsync(string email, CancellationToken cancellationToken)
        {
            return _userManager.Users.AnyAsync(x => x.Email == email && x.EmailConfirmed, cancellationToken);
        }

        public async Task<UserAuthResetPasswordResponseDto> ForgotPasswordAsync(string email, CancellationToken cancellationToken)
        {

            var user = await _userManager.FindByEmailAsync(email);

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);

            return new UserAuthResetPasswordResponseDto(user.Id, user.Email, user.FirstName, token);

        }

        public async Task<bool> ResetPasswordAsync(UserAuthResetPasswordCommand command, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(command.Email);
            var decodedToken = HttpUtility.UrlDecode(command.Token);


            var result = await _userManager.ResetPasswordAsync(user, decodedToken, command.Password);
            if (!result.Succeeded)
            {
                throw new Exception("Password reset failed");
            }



            return true;
        }

        public async Task<bool> ChangePasswordAsync(Guid userId, string currentPassword, string newPassword, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            var result = await _userManager.ChangePasswordAsync(user, currentPassword, newPassword);
            if (!result.Succeeded)
            {
                throw new Exception("Password change failed");
            }

            return true;


        }

        public async Task<JwtDto> SocialLoginAsync(UserAuthSocialLoginCommand command, CancellationToken cancellationToken)
        {
            User? user;

            user = await _userManager.FindByEmailAsync(command.Email);

            if (user is null)
            {
                user = UserAuthSocialLoginCommand.ToUser(command);

                var result = await _userManager.CreateAsync(user);

                if (!result.Succeeded)
                    throw new Exception("User registration failed");
            }

            return await _jwtService.GenerateTokenAsync(user.Id, user.Email, cancellationToken);
        }

        public Task<bool> GenerateForgetPasswordTokenAsync(string email, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<UserGetProfileDto> GetProfileAsync(CancellationToken cancellationToken)
        {
            var user = await _userManager
                .FindByIdAsync(_currentUserService.UserId.ToString());

            user.Balance = await _applicationDbContext
                .UserBalances
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.UserId == user.Id, cancellationToken);

            return UserGetProfileDto.Map(user);
        }
    }
}
