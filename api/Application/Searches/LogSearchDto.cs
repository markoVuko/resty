using Application.Queries;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Searches
{
    public class LogSearchDto : PagedQuery
    {
        public int LogId { get; set; }
        public int ActorId { get; set; }
        public string ActionName { get; set; }
    }
}
