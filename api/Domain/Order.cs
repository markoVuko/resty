using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class Order : Entity
    {
        public virtual User User { get; set; }
        public int UserId { get; set; }
        public virtual ICollection<OrderLine> OrderLines { get; set; }
        public int TableNumber { get; set; }
    }
}
