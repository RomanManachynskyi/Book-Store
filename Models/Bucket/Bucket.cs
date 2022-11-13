using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Book_Store.Models.Bucket
{
    public class Bucket
    {
        public int Id { get; set; }
        public Models.User.User User { get; set; }
    }
}