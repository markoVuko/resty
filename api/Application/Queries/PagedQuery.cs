using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Queries
{
    public abstract class PagedQuery
    {
        public int PerPage { get; set; } = 7;
        public int Page { get; set; } = 1;
    }
}
