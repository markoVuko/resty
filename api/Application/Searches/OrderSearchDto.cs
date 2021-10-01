using Application.Queries;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Searches
{
    public class OrderSearchDto : PagedQuery
    {
        public int UserId { get; set; }
        public int TablesFrom { get; set; }
        public int TablesTo { get; set; }
        public decimal TotalPriceFrom { get; set; }
        public decimal TotalPriceTo { get; set; }
    }
}
