using MediatR;
using MextFullstackSaaS.Application.Common.Interfaces;
using MextFullstackSaaS.Application.Common.Models;
using MextFullstackSaaS.Application.Common.Translations;
using MextFullstackSaaS.Application.Features.UserAuth.Commands.login;
using Microsoft.Extensions.Localization;

namespace MextFullstackSaaS.Application.Handlers;

public class UserAuthLoginCommandHandler : IRequestHandler<UserAuthLoginCommand, ResponseDto<JwtDto>>
{
    private readonly IIdentityService _identityService;
    private readonly IJwtService _jwtService;
    private readonly IStringLocalizer<CommonTranslations> _localizer;

    public UserAuthLoginCommandHandler(IIdentityService identityService,IStringLocalizer<CommonTranslations> localizer )
    {
        _identityService = identityService;
        _localizer = localizer;

    }

    public async Task<ResponseDto<JwtDto>> Handle(UserAuthLoginCommand request, CancellationToken cancellationToken)
    {
        var jwtDto = await _identityService.LoginAsync(request, cancellationToken);

        return new ResponseDto<JwtDto>(jwtDto, "Login successful");
    }
}