using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Book_Store.Dtos.Product_Entities.Product;
using Book_Store.Models.Product_Entities;

namespace Book_Store.Dtos.Bucket
{
    public class GetBucketItemsDto
    {
        public int Id { get; set; }
        public GetProductsDto Book { get; set; }
    }
}