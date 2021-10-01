using Application.Queries;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Searches
{
    public class WorkTypeSearchDto : PagedQuery
    {
        public string Name { get; set; }
    }
}
