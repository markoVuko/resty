using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTO
{
    public class OrderDto
    {
        public int Id { get; set; }
        public string EmployeeFullName { get; set; }
        public int UserId { get; set; }
        public virtual ICollection<OrderLineDto> OrderLines { get; set; }
        public int TableNumber { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}
