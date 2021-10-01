using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class WorkType : Entity
    {
        public string Name { get; set; }
        public decimal HourlyRate { get; set; }
        public virtual ICollection<Schedule> Schedules { get; set; }
    }
}
