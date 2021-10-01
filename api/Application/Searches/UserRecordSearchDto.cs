using Application.Queries;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Searches
{
    public class UserRecordSearchDto : PagedQuery
    {
        public string Keyword { get; set; }
        public int UserId { get; set; }
        public int RecordTypeId { get; set; }
    }
}
