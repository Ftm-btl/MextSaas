using MediatR;
using MextFullstackSaaS.Application.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MextFullstackSaaS.Application.Features.Payments.Commands.CreatePaymentForm
{
    public class PaymentsCreatePaymentFormCommand : IRequest<ResponseDto<PaymentsCreatePaymentFormDto>>
    {
        public int Credits { get; set; }
    }
}
