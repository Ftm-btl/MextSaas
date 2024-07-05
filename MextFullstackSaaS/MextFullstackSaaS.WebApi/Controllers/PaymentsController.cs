using MediatR;
using MextFullstackSaaS.Application.Features.Payments.Commands.CompletePayment;
using MextFullstackSaaS.Application.Features.Payments.Commands.CreatePaymentForm;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MextFullstackSaaS.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentsController : ControllerBase
    {

        private readonly ISender _mediator;

        public PaymentsController(ISender mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreatePaymentFormAsync(PaymentsCreatePaymentFormCommand command,CancellationToken cancellationToken)
        {
            return Ok(await _mediator.Send(command, cancellationToken));
        }

        [HttpPost("complete-paymnet")]
        public async Task<IActionResult> PaymentResultAsync([FromForm] string token, CancellationToken cancellationToken)
        {
            var response=await _mediator.Send(new PaymentsCompletePaymentCommand(token),cancellationToken);

            if (!response.Data)
                return Redirect($"http://localhost:5180/payment-faild");

            return Redirect($"http://localhost:5180/payment-success");
        }

    }
}
