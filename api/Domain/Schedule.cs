using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class Schedule : Entity
    {
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
        public DateTime? DateBegun { get; set; }
        public DateTime? DateFin { get; set; }
        public virtual User User { get; set; }
        public int UserId { get; set; }
        public virtual WorkType WorkType { get; set; }
        public int WorkTypeId { get; set; }
        public string BossFullName { get; set; }
    }
}
