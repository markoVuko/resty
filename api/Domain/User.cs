using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class User : Entity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Address { get; set; }
        public virtual Role Role { get; set; }
        public int RoleId { get; set; }
        public virtual ICollection<UserRecord> UserRecords { get; set; }
        public virtual ICollection<Schedule> Schedules { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}
