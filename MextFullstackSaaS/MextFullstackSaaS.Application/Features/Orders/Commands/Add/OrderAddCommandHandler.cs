using MediatR;
using MextFullstackSaaS.Application.Common.Helpers;
using MextFullstackSaaS.Application.Common.Interfaces;
using MextFullstackSaaS.Application.Common.Models;
using MextFullstackSaaS.Application.Common.Models.OpenAI;
using MextFullstackSaaS.Application.Features.Orders.Queries.GetAll;
using Microsoft.Extensions.Caching.Memory;

namespace MextFullstackSaaS.Application.Features.Orders.Commands.Add
{
    public class OrderAddCommandHandler: IRequestHandler<OrderAddCommand, ResponseDto<Guid>>
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly ICurrentUserService _currentUserService;
        private readonly IOpenAIService _openAIService;
        private readonly IMemoryCache _memoryCache;
        private readonly IOrderHubService _orderHubService;
        private readonly IObjectStorageService _objectStorageService;

        public OrderAddCommandHandler(IApplicationDbContext dbContext, ICurrentUserService currentUserService, IOpenAIService openAiService, IMemoryCache memoryCache, IOrderHubService orderHubService,IObjectStorageService objectStorageService)
        {
            _dbContext = dbContext;
            _currentUserService = currentUserService;
            _openAIService = openAiService;
            _memoryCache = memoryCache;
            _orderHubService = orderHubService;
            _objectStorageService = objectStorageService;
        }

        public async Task<ResponseDto<Guid>> Handle(OrderAddCommand request, CancellationToken cancellationToken)
        {
           var order = OrderAddCommand.MapToOrder(request);

           order.UserId = _currentUserService.UserId;
           order.CreatedByUserId = _currentUserService.UserId.ToString();
           var base64Images = await _openAIService.DAllECreateIconAsync(DallECreateIconRequestDto.MapFromOrderAddCommand(request), cancellationToken);
           order.Urls=await _objectStorageService.UploadImagesAsync(base64Images, cancellationToken);
            /* TODO: Make Request to the Gemine or Dall-e 3 */

            _dbContext.Orders.Add(order);

            await _dbContext.SaveChangesAsync(cancellationToken);

            if (_memoryCache.TryGetValue(MemoryCacheHelper.GetOrdersGetAllKey(_currentUserService.UserId), out List<OrderGetAllDto> orders))
            {
                orders.Add(OrderGetAllDto.FromOrder(order));

                _memoryCache.Set(MemoryCacheHelper.GetOrdersGetAllKey(_currentUserService.UserId),orders,MemoryCacheHelper.GetMemoryCacheEntryOptions());   
            }

            await _orderHubService.NewOrderAddedAsync(order.Urls, cancellationToken);

            return new ResponseDto<Guid>(order.Id,"Your order completed successfully.");
        }
    }
}
