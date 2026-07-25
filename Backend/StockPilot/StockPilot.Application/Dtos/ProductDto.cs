using StockPilot.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace StockPilot.Application.Dtos
{
    public class ProductDto
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Sku { get; set; }

        public Guid? CategoryId { get; set; }

        public string? Unit { get; set; }

        public decimal CostPrice { get; set; }
        public decimal SellingPrice { get; set; }

        public int? CurrentStock { get; set; }
        public int? MinimumStock { get; set; }
        public int ReorderQuantity { get; set; }

        public string? ImageUrl { get; set; }
    }
}
