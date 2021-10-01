using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class Item : Entity
    {
        public string Name { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public int MinQuantity { get; set; }
        public virtual Supplier Supplier { get; set; }
        public int SupplierId { get; set; }
        public virtual ICollection<CategoryItem> CategoryItems { get; set; }
        public virtual ICollection<OrderLine> OrderLines { get; set; }
    }
}
