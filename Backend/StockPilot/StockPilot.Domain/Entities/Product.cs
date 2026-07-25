using StockPilot.Domain.Common;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection.PortableExecutable;
using System.Text;

namespace StockPilot.Domain.Entities
{
    public class Product : AuditableEntity
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Sku { get; set; }
        
        public Guid? CategoryId { get; set; }
        public Category? Category { get; set; }

        public string? Unit { get; set; }

        public decimal CostPrice { get; set; }
        public decimal SellingPrice { get; set; }

        public int? CurrentStock { get; set; }
        public int? MinimumStock { get; set; } = 10;
        public int ReorderQuantity { get; set; }

        public string? ImageUrl { get; set; }
        
        public bool IsActive { get; set; }
        public Product() { }

        public static Product Create(string productName,string sku, int CurrentStock, int MinimumStock,string ImageUrl, decimal CostPrice, string Unit, Guid CategoryId) 
        {
            return new Product
            {
                Name = productName,
                Sku = sku,
                CurrentStock = CurrentStock,
                MinimumStock = MinimumStock,
                ImageUrl = ImageUrl,
                CostPrice = CostPrice,
                Unit = Unit,
                CategoryId = CategoryId
            };
        }
        public void Update(string? productName, string sku, int? currentStock, int? MinimumStock, decimal? CostPrice, string Unit)
        {
            Name = productName ?? Name;
            Sku = sku ?? Sku;
            CurrentStock = currentStock != 0 ? currentStock : CurrentStock;
            this.MinimumStock = MinimumStock != 0 ? MinimumStock : 0;
            this.CostPrice = CostPrice != 0 ? this.CostPrice : 0;
            this.Unit = Unit ?? this.Unit;

        }
    }

  

}
