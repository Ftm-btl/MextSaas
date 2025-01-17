﻿using MediatR;
using MextFullstackSaaS.Application.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MextFullstackSaaS.Application.Features.UserAuth.Commands.login
{
    public class UserAuthLoginCommand : IRequest<ResponseDto<JwtDto>>
    {
        public string Email { get; set; }
        public string Password { get; set; }


    }
}