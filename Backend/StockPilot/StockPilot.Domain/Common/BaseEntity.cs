using StockPilot.Domain.Entities.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace StockPilot.Domain.Common
{
    public  abstract class BaseEntity 
    {
        public Guid Id { get; set; }
    }
}
