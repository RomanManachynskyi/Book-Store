using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace Book_Store.Models.Bucket
{
    public class Bucket
    {
        public int Id { get; set; }

        public int UserId { get; set; }
        public Models.User.User User { get; set; }

        public int BookId { get; set; }
        public Product_Entities.Book Book { get; set; }       
    }
}