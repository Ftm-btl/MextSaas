using MextFullstackSaaS.Application.Common.Interfaces;
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

namespace MextFullstackSaaS.Infrastructure.Services
{
    public class IdentityManager : IIdentityService
    {
        private readonly IJwtService _jwtService;
        private readonly UserManager<User> _userManager;
        private readonly IEmailService _emailService;

        public IdentityManager(UserManager<User> userManager, IJwtService jwtService, IEmailService emailService)
        {
            _userManager = userManager;
            _jwtService = jwtService;
            _emailService = emailService;

        }

        public Task<bool> CheckIfEmailVerifiedAysnc(string email, CancellationToken cancellationToken)
        {
            return _userManager.Users.AnyAsync(x => x.Email == email && x.EmailConfirmed, cancellationToken);
        }

        public async Task<bool> CheckPasswordSignInAsync(string email, string password, CancellationToken cancellationToken)
        {
            var user=await _userManager.FindByNameAsync(email);

            if (user is null) return false;


            return await _userManager.CheckPasswordAsync(user,password);
        }

        public async Task<bool> IsEmailExistsAsync(string email, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user is not null)
                return true;

            return false;

        }

        public async Task<JwtDto> LoginAsync(UserAuthLoginCommand command, CancellationToken cancellationToken)
        {
            var user=await _userManager.FindByEmailAsync(command.Email);

            var jwtDto = await _jwtService.GenerateTokenAsync(user.Id, user.Email, cancellationToken);

            return jwtDto;
        }

        public async Task<UserAuthRegisterResponseDto> RegisterAsync(UserAuthRegisterCommand command, CancellationToken cancellationToken)
        {
            var user = UserAuthRegisterCommand.ToUser(command);

            var result = await _userManager.CreateAsync(user, command.Password);

            if (!result.Succeeded)
            {
                throw new Exception("User registration failed");
            }

            var token=await _userManager.GenerateEmailConfirmationTokenAsync(user);

            return new UserAuthRegisterResponseDto(user.Id, user.Email, user.FirstName, token);
        }


        public async Task<bool> GenerateForgetPasswordTokenAsync(string email, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(email);
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            // Burada, şifre sıfırlama bağlantısını e-posta ile gönderme kodunu ekleyin.
            await _emailService.SendPasswordResetLinkAsync(email, token, cancellationToken);

            return true;
        }

        public async Task<bool> VerifyEmailAsync(UserAuthVerifyEmailCommand command, CancellationToken cancellationToken)
        {            
            var user = await _userManager.FindByEmailAsync(command.Email);

            var decodedToken = HttpUtility.UrlDecode(command.Token);
            

            var result = await _userManager.ConfirmEmailAsync(user, decodedToken);

            
            
            if (!result.Succeeded)
            {
                throw new Exception("User email");
            }
            return true;
        }

        public async Task<bool> ResetPasswordAsync(string email, string token, string newPassword, CancellationToken cancellationToken)
        {
            var user= await _userManager.FindByIdAsync(email);
            var result = await _userManager.ResetPasswordAsync(user, token, newPassword);

            await _emailService.ResetPasswordAsync(email, token,newPassword, cancellationToken);
            return result.Succeeded;
        }
    }
}
