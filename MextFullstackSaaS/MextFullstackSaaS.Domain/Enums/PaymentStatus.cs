﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MextFullstackSaaS.Domain.Enums
{
    public enum PaymentStatus
    {
        Initiated=1,
        Pending=2,
        Success=3,
        Failed=4,
        Canceled=5,
        Refunded=6
    }
}
