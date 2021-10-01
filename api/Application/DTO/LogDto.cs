using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTO
{
    public class LogDto
    {
        public int Id { get; set; }
        public int ActorId { get; set; }
        public string ActionName { get; set; }
        public string Description { get; set; }
    }
}
