using FluentValidation;
using MextFullstackSaaS.Application.Common.Interfaces;
using MextFullstackSaaS.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MextFullstackSaaS.Application.Features.Orders.Commands.Update
{
    public class OrderUpdateCommandValidator : AbstractValidator<OrderUpdateCommand>
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IApplicationDbContext _dbContext;

        public OrderUpdateCommandValidator(ICurrentUserService currentUserService, IApplicationDbContext dbContext)
        {
            _currentUserService = currentUserService;
            _dbContext = dbContext;

            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage("Order Id update");     
;

            RuleFor(x => x.ColourCode)
                .NotEmpty()
                .MaximumLength(15)
                .WithMessage("The colour code must be less than 15 characters.");

            RuleFor(x => x.Model)
                .IsInEnum()
                .WithMessage("Please select a valid model.");


            RuleFor(x => x.Size)
                .IsInEnum()
                .WithMessage("Please select a valid size.");

            RuleFor(x => x.Shape)
                .IsInEnum()
                .WithMessage("Please select a valid shape.");

            RuleFor(x => x.Quantity)
                .GreaterThan(0)
                .LessThanOrEqualTo(6)
                .WithMessage("Please select a valid quantity.");

            RuleFor(x => x.Id)
                .MustAsync(IsUserIdValid)
                .WithMessage("You need to be logged-in to place an order.");

            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage("You need to be logged-in to place an order.");
            
            RuleFor(x => x.Id)
                .MustAsync(IsTheSameUserAsync)
                .WithMessage("You need to be logged-in to place an order.");
        }

        private Task<bool> IsUserIdValid(Guid id, CancellationToken cancellationToken)
        {
            return _dbContext.Orders.AnyAsync(o => o.Id == id, cancellationToken);
        }

        private Task<bool> IsTheSameUserAsync(Guid id, CancellationToken cancellationToken)
        {

            return _dbContext.Orders
                .Where(x => x.Id == _currentUserService.UserId)
                .AnyAsync(x => x.Id == id, cancellationToken);


        }
    }
}
