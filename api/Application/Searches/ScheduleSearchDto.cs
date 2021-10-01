using Application.Queries;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Searches
{
    public class ScheduleSearchDto : PagedQuery
    {
        public string Keyword { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public int UserId { get; set; }
        public int WorkTypeId { get; set; }
        public int Status { get; set; } = -1;
    }
}
