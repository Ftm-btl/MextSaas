using MediatR;
using MextFullstackSaaS.Application.Common.Interfaces;
using MextFullstackSaaS.Application.Common.Models;
using MextFullstackSaaS.Application.Common.Models.Payments;
using MextFullstackSaaS.Domain.Entities;
using MextFullstackSaaS.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MextFullstackSaaS.Application.Features.Payments.Commands.CompletePayment
{
    public class PaymentsCompletePaymentCommandHandler : IRequestHandler<PaymentsCompletePaymentCommand, ResponseDto<bool>>
    {
        private readonly IPaymentService _paymentService;
        private readonly IApplicationDbContext _applicationDbContext;

        public PaymentsCompletePaymentCommandHandler(IPaymentService paymnetService, IApplicationDbContext applicationDbContext)
        {
            _paymentService = paymnetService;
            _applicationDbContext = applicationDbContext;
        }

        public async Task<ResponseDto<bool>> Handle(PaymentsCompletePaymentCommand request, CancellationToken cancellationToken)
        {
            var userPayment=await _applicationDbContext
                .UserPayments
                .FirstOrDefaultAsync(x=>x.Token==request.Token, cancellationToken);

            var checkResponse = _paymentService.CheckPaymentByToken(request.Token);

            UpdateUserPayment(ref userPayment);

            var userPaymentHistory = CreateUserPaymentHistory(userPayment, checkResponse);

            _applicationDbContext.UserBalanceHistories.Add(userPaymentHistory);



            await _applicationDbContext.SaveChangesAsync(cancellationToken);

            return new ResponseDto<bool>(checkResponse.IsSuccess, "Your payment process has been successful!");
        }

        private void UpdateUserPayment(ref UserPayment userPayment)
        {
            userPayment.Status=PaymentStatus.Success;
            userPayment.ModifiedByUserId=userPayment.UserId.ToString();
            userPayment.ModifiedOn=DateTimeOffset.UtcNow;
        }

        private UserPaymentHistory CreateUserPaymentHistory(UserPayment userPayment, PaymentsCheckPaymentByTokenResponse checkPaymentByTokenResponse)
        {
            return new UserPaymentHistory
            {
                Id = Guid.NewGuid(),
                ConversationId = checkPaymentByTokenResponse.ConversationId,
                CreatedByUserId = userPayment.UserId.ToString(),
                CreatedOn = DateTimeOffset.UtcNow,
                Status = PaymentStatus.Success,
                UserPaymentId = userPayment.Id
            };
        }
    }
}
    