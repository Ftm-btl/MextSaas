﻿using MextFullstackSaaS.Domain.Common;
using MextFullstackSaaS.Domain.Enums;
using MextFullstackSaaS.Domain.Identity;
using MextFullstackSaaS.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MextFullstackSaaS.Domain.Entities
{
    public class UserPayment:EntityBase<Guid>
    {
        
        public string BasketId { get; set; }
        public string Token {  get; set; }
        public decimal Price { get; set; }
        public decimal PaidPrice { get; set; }
        public CurrencyCode CurrencyCode { get; set; }
        public string Ip {  get; set; }
        public PaymentStatus Status { get; set; }

        public Guid UserId { get; set; }
        public User User { get; set; }

        public string? Note { get; set; }
        public decimal? RefundedAmount { get; set; }

        public UserPaymentDetail UserPaymentDetail { get; set; }

        public ICollection<UserPaymentHistory> Histories { get; set; }
    }
}
