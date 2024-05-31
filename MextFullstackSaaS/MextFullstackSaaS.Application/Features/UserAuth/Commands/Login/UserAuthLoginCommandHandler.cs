using MediatR;
using MextFullstackSaaS.Application.Common.Interfaces;
using MextFullstackSaaS.Application.Common.Models;
using MextFullstackSaaS.Application.Features.UserAuth.Commands.login;

namespace MextFullstackSaaS.Application.Handlers;

public class UserAuthLoginCommandHandler : IRequestHandler<UserAuthLoginCommand, ResponseDto<JwtDto>>
{
    private readonly IIdentityService _identityService;
    private readonly IJwtService _jwtService;

    public UserAuthLoginCommandHandler(IIdentityService identityService)
    {
        _identityService = identityService;

    }

    public async Task<ResponseDto<JwtDto>> Handle(UserAuthLoginCommand request, CancellationToken cancellationToken)
    {
        var jwtDto = await _identityService.LoginAsync(request, cancellationToken);

        return new ResponseDto<JwtDto>(jwtDto, "Login successful");
    }
}