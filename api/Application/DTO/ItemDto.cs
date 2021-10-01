using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTO
{
    public class ItemDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public int MinQuantity { get; set; }
        public string Supplier { get; set; }
        public int SupplierId { get; set; }
        public virtual ICollection<CategoryDto> Categories { get; set; }
    }
}
