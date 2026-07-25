using System;
using System.Collections.Generic;
using System.Text;

namespace StockPilot.Application.Features.Queries.Category.ListingCategory
{
    public class ListingCategoryQueryDto
    {
        public Guid? CategoryId { get; set; }
        public string? CategoryName { get; set; }
        public string? Description { get; set; }
        public int ProductCount {  get; set; }
        public bool IsActive { get; set; }
    }
}
