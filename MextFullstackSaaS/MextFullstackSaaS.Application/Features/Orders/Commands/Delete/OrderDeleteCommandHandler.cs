using MediatR;
using MextFullstackSaaS.Application.Common.Interfaces;
using MextFullstackSaaS.Application.Common.Models;
using Microsoft.EntityFrameworkCore;

namespace MextFullstackSaaS.Application.Features.Orders.Commands.Delete
{
    public class OrderDeleteCommandHandler:IRequestHandler<OrderDeleteCommand, ResponseDto<Guid>>
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly ICurrentUserService _currentUserService;

        public OrderDeleteCommandHandler(IApplicationDbContext dbContext,ICurrentUserService currentUserService)
        {
            _dbContext = dbContext;
            _currentUserService=currentUserService;
        }

        public async Task<ResponseDto<Guid>> Handle(OrderDeleteCommand request, CancellationToken cancellationToken)
        {
            var order = await _dbContext
                .Orders
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            _dbContext.Orders.Remove(order);

            await _dbContext.SaveChangesAsync(cancellationToken);

            return new ResponseDto<Guid>(order.Id); // The selected order has been deleted successfully.
        }
    }
}
