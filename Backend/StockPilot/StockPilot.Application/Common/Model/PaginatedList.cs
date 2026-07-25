using System;
using System.Collections.Generic;
using System.Text;

namespace StockPilot.Application.Common.Model
{
    public class PaginatedList<T> 
    {
        public int pageNumber { get; set; }
        public int pageSize { get; set; }
        public List<T>? value { get; set; }
        public int totalCount { get; set; }
    }
}
