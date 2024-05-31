using MediatR;
using MextFullstackSaaS.Application.Features.UserAuth.Commands.login;
using MextFullstackSaaS.Application.Features.UserAuth.Commands.Register;
using Microsoft.AspNetCore.Mvc;

namespace MextFullstackSaaS.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserAuthsController : ControllerBase
    {
        private readonly ISender _mediatr;

        public UserAuthsController(ISender mediatr)
        {
            _mediatr = mediatr;
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
       
    }
}
