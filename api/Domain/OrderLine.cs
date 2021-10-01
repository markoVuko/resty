using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class OrderLine : Entity
    {
        public virtual Item Item { get; set; }
        public int ItemId { get; set; }
        public string ItemName { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public virtual Order Order { get; set; }
        public int OrderId { get; set; }
    }
}
