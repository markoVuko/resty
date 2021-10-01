using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class Supplier : Entity
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Phone { get; set; }
        public string Mail { get; set; }
        public virtual ICollection<Item> Items { get; set; }
    }
}
