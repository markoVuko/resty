﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTO
{
    public class UserDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Address { get; set; }
        public int RoleId { get; set; }
        public string Role { get; set; }
    }
}
