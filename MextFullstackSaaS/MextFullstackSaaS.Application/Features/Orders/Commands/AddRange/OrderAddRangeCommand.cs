using MediatR;
using MextFullstackSaaS.Application.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MextFullstackSaaS.Application.Features.Orders.Commands.AddRange
{
    public class OrderAddRangeCommand :IRequest<ResponseDto<int>>
    {
        public int OrderCount { get; set; } = 50000;
    }
}
