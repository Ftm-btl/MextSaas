﻿using FluentValidation;
using MextFullstackSaaS.Application.Common.Interfaces;
using MextFullstackSaaS.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace MextFullstackSaaS.Application.Features.Orders.Commands.Add
{
    public class OrderAddCommandValidator: AbstractValidator<OrderAddCommand>
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IApplicationDbContext _applicationDbContext;

        public OrderAddCommandValidator(ICurrentUserService currentUserService, IApplicationDbContext applicationDbContext)
        {
            _currentUserService = currentUserService;
            _applicationDbContext = applicationDbContext;

            RuleFor(x => x.IconDescription)
                .NotEmpty()
                .MaximumLength(200)
                .WithMessage("The icon description must be less than 200 characters.");

            RuleFor(x => x.ColourCode)
                .NotEmpty()
                .MaximumLength(15)
                .WithMessage("The colour code must be less than 15 characters.");

            RuleFor(x => x.Model)
                .IsInEnum()
                .WithMessage("Please select a valid model.");

            RuleFor(x => x.DesignType)
                .IsInEnum()
                .WithMessage("Please select a valid design type.");

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

            RuleFor(x => x.Size)
                .Must(IsUserIdValid)
                .WithMessage("You need to be logged-in to place an order.");

            RuleFor(x => x)
                .MustAsync(HasCreditsAysnc)
                .WithMessage("You need to hane at least 1 credit to place an order.");
           
        }

        private bool IsUserIdValid(IconSize size) => _currentUserService.UserId != Guid.Empty;

        // Minimum Viable Product

        private Task<bool> HasCreditsAysnc(OrderAddCommand command,CancellationToken cancellationToken)
        {
            return _applicationDbContext
                .UserBalances
                .Where(x => x.UserId == _currentUserService.UserId)
                .AnyAsync(x => x.Credits >= 1, cancellationToken);
        }
    }
}
