using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Book_Store.Models.User
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Mail { get; set; } = string.Empty;
        public byte[] PaswordHash { get; set; }
        public byte[] PaswordSalt { get; set; }
        public Role Role { get; set; }
        public List<Bucket.Bucket>? Bucket { get; set; }
        public List<Orders.Orders>?  Order { get; set; }
    }
}