using System;
using System.Collections.Generic;
using System.Text;

namespace StockPilot.Application.Features.Queries.Supplier.GetSupplier
{
    public class SupplierListItemDto
    {
        public Guid SupplierId { get; set; }
        public string? Name { get; set; }
        public string? ContactPerson { get; set; }
        public bool IsActive { get; set; }
        public int SuppliedProductCount { get; set; }
        public int PurchaseOrderCount { get; set; }

    }

}
