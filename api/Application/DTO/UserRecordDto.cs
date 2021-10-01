using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTO
{
    public class UserRecordDto
    {
        public int Id { get; set; }
        public virtual UserDto User { get; set; }
        public int UserId { get; set; }
        public string RecordType { get; set; }
        public decimal PayChange { get; set; }
        public int RecordTypeId { get; set; }
        public string Comment { get; set; }
    }
}
