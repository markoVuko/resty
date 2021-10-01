using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTO
{
    public class RegisterUserDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Address { get; set; }
    }
}
