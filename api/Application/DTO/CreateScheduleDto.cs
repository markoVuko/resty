using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTO
{
    public class CreateScheduleDto
    {
        public int Id { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
        public DateTime DateBegun { get; set; }
        public DateTime DateFin { get; set; }
        public int UserId { get; set; }
        public int WorkTypeId { get; set; }
        public string WorkType { get; set; }
        public string BossFullName { get; set; }
        public bool ClockedOut { get; set; }
        public bool BossEdit { get; set; }
        public decimal HourlyRate { get; set; }
    }
}
