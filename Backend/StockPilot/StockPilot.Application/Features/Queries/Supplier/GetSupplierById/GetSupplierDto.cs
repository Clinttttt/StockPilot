using System;
using System.Collections.Generic;
using System.Text;

namespace StockPilot.Application.Features.Queries.Supplier.GetSupplierById
{
    public class GetSupplierDto
    {
        public Guid SupplierId { get; set; }
        public DateTime? LastOrder {  get; set; }
        public bool IsActive { get; set; } = true;
        public string FullName { get; set; } = string.Empty;
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }
        public int SuppliedProductCount { get; set; }
        public int PurchaseOrderCount { get; set; }

    }
}
