using Google.Apis.Auth.OAuth2.Flows;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Auth;
using MediatR;
using MextFullstackSaaS.Application.Features.UserAuth.Commands.ForgetPassword;
using MextFullstackSaaS.Application.Features.UserAuth.Commands.login;
using MextFullstackSaaS.Application.Features.UserAuth.Commands.Register;
using MextFullstackSaaS.Application.Features.UserAuth.Commands.ResetPassword;
using MextFullstackSaaS.Application.Features.UserAuth.Commands.VerifyEmail;
using MextFullstackSaaS.Domain.Settings;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace MextFullstackSaaS.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersAuthController : ControllerBase
    {
        private readonly ISender _mediatr;
        private readonly GoogleSettings _googleSettings;
        private const string RedirectUri = "https://localhost:7281/api/UsersAuth/signin-google";
        private readonly string _googleAuthorizationUrl;

        public UsersAuthController(ISender mediatr, IOptions<GoogleSettings> googleSettings)
        {
            _mediatr = mediatr;

            _googleSettings = googleSettings.Value;

            _googleAuthorizationUrl = $"https://accounts.google.com/o/oauth2/v2/auth?" +
                                         $"client_id={_googleSettings.ClientId}&" +
                                         $"response_type=code&" +
                                         $"scope=openid%20email%20profile&" +
                                         $"access_type=offline&" +
                                         $"redirect_uri={RedirectUri}";
        }

        [HttpGet("signin-google-start")]
        public IActionResult GoogleSignInStart()
         => Redirect(_googleAuthorizationUrl);

        [HttpGet("signin-google")]
        public async Task<IActionResult> SignInGoogleAsync(string code, CancellationToken cancellationToken)
        {
            var flow = new GoogleAuthorizationCodeFlow(new GoogleAuthorizationCodeFlow.Initializer()
            {
                ClientSecrets = new ClientSecrets()
                {
                    ClientId = _googleSettings.ClientId,
                    ClientSecret = _googleSettings.ClientSecret,
                }
            });

            var tokenResponse = await flow.ExchangeCodeForTokenAsync(
                userId: "user",
                code: code,
                redirectUri: RedirectUri,
                cancellationToken
            );

            var payload = await GoogleJsonWebSignature.ValidateAsync(tokenResponse.IdToken);

            var email = payload.Email;
            var firstName = payload.GivenName;
            var lastName = payload.FamilyName;



            //var jwtDto =
            //    await _authenticationService.SocialLoginAsync(userEmail, firstName, lastName, cancellationToken);

            //var queryParams = new Dictionary<string, string>()
            //{
            //    {"access_token",jwtDto.AccessToken },
            //    {"expiry_date",jwtDto.ExpiryDate.ToBinary().ToString() },
            //};

            //var formContent = new FormUrlEncodedContent(queryParams);

            //var query = await formContent.ReadAsStringAsync(cancellationToken);

            //var redirectUrl = $"http://127.0.0.1:5173/social-login?{query}";

            return Redirect($" http://localhost:5180/social-login?email={email}&firstName={firstName}&lastName={lastName}");
        }


        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync(UserAuthRegisterCommand command,CancellationToken cancellationToken)
        {
            //throw new ArgumentNullException(command.FirstName, "First name is required");

            return Ok(await _mediatr.Send(command, cancellationToken));

        }
        [HttpPost("login")]
        public async Task<IActionResult> LoginAsyn(UserAuthLoginCommand command,CancellationToken cancellationToken)
        {
            //throw new ArgumentNullException(command.FirstName, "First name is required");

            return Ok(await _mediatr.Send(command, cancellationToken));

        }
        [HttpGet("verify-email")]
        public async Task<IActionResult> VerifyEmailAsync([FromQuery] UserAuthVerifyEmailCommand command, CancellationToken cancellationToken)
        {
            return Ok(await _mediatr.Send(command, cancellationToken));
        }
        [HttpPost("forget-password")]
        public async Task<IActionResult> ForgetPasswordAysnc( UserAuthForgetPasswordCommand command,CancellationToken cancellationToken)
        {
            //throw new ArgumentNullException(command.FirstName, "First name is required");

            return Ok(await _mediatr.Send(command, cancellationToken));

        }
        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPasswordAysnc( UserAuthResetPasswordCommand command,CancellationToken cancellationToken)
        {

            return Ok(await _mediatr.Send(command, cancellationToken));

        }
       
    }
}
