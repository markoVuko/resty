using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class UserRecord : Entity
    {
        public virtual User User { get; set; }
        public int UserId { get; set; }
        public virtual RecordType RecordType { get; set; }
        public int RecordTypeId { get; set; }
        public string Comment { get; set; }
    }
}
