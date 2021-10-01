using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class RecordType : Entity
    {
        public string Name { get; set; }
        public decimal PayChange { get; set; }
        public virtual ICollection<UserRecord> UserRecords { get; set; }
    }
}
