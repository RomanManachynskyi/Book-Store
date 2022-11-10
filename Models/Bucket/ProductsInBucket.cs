using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Book_Store.Models.Bucket
{
    public class ProductsInBucket
    {
        public int Id { get; set; }
        public Bucket Bucket { get; set; }
        public Product_Entities.Book Book { get; set; }
    }
}