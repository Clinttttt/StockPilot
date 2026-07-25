using System;
using System.Collections.Generic;
using System.Text;

namespace StockPilot.Application.Features.Queries.Product.ListingProduct
{
    public class ListingProductQueryDto
    {
        public Guid ProductId { get; set; }
        public Guid? CategoryId { get; set; }
        public string? ProductName { get; set; }
        public string? ImageUrl { get; set; }
        public int? CurrentStock { get; set; }
        public decimal? SellingPrice { get; set; }
        public bool IsActive { get; set; }
    }
   
}
