using Application.Queries;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Searches
{
    public class SupplierSearchDto : PagedQuery
    {
        public string Keyword { get; set; }
        public string Phone { get; set; }
    }
}
