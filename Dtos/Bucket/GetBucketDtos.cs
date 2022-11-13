using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Book_Store.Models.Product_Entities;

namespace Book_Store.Dtos.Bucket
{
    public class GetBucketDtos
    {
        public int Id { get; set; }
        public Models.Product_Entities.Book Book { get; set; }
    }
}