using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace Book_Store.Models.Bucket
{
    public class ProductsInBucket
    {
        public int Id { get; set; }
        public Bucket Bucket { get; set; }

        [ForeignKey("Book")]
        public int BookId { get; set; }

        public Product_Entities.Book Book { get; set; }
    }
}