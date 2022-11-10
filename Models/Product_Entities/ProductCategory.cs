using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Book_Store.Models.Product_Entities
{
    public class ProductCategory
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public int GoodsQuantity { get; set; }
        public string PhotoLocation { get; set; }
    }
}