using MediatR;
using MextFullstackSaaS.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MextFullstackSaaS.Application.Features.Payments.Commands.CreatePaymentForm
{
    public class PaymentsCreatePaymentFormCommandHandler : IRequestHandler<PaymentsCreatePaymentFormCommand, object>
    {
        private readonly IPaymentServices _paymentServices;

        public PaymentsCreatePaymentFormCommandHandler(IPaymentServices paymentServices)
        {
            _paymentServices = paymentServices;
        }

        public Task<object> Handle(PaymentsCreatePaymentFormCommand request, CancellationToken cancellationToken)
        {
            return _paymentServices.CreateCheckoutFormAsync(cancellationToken);
        }
    }
}
