using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class CategoryItem : Entity
    {
        public virtual Category Category { get; set; }
        public int CategoryId { get; set; }
        public virtual Item Item { get; set; }
        public int ItemId { get; set; }
    }
}
