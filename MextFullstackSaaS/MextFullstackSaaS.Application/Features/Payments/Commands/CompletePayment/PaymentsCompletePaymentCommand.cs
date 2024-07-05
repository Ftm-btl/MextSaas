using MediatR;
using MextFullstackSaaS.Application.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MextFullstackSaaS.Application.Features.Payments.Commands.CompletePayment
{
    public class PaymentsCompletePaymentCommand:IRequest<ResponseDto<bool>>
    {
        public string Token { get; set; }

        public PaymentsCompletePaymentCommand(string token)
        {  
            Token = token; 
        }

        public PaymentsCompletePaymentCommand() { }
    }
}
