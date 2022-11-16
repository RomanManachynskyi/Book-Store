using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Book_Store.Models.User;

namespace Book_Store.Dtos.User
{
    public class UserRegisterDto
    {
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Mail { get; set; } = string.Empty;
        public Role Role { get; set; } = Role.Customer;
    }
}