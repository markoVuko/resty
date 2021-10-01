using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTO
{
    public class OrderLineDto
    {
        public int ItemId { get; set; }
        public string ItemName { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
