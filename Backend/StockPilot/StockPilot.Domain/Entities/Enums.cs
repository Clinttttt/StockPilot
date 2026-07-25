using System;
using System.Collections.Generic;
using System.Text;

namespace StockPilot.Domain.Entities
{
    public class Enums
    {
        public enum UserRole
        {
            Admin,
            Staff,
            Viewer,
        }
        public enum StockAdjustmentType
        {
            Increase,
            Decrease,
        }
    }
}
