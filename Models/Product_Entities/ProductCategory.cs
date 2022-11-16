using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Book_Store.Models.User;

namespace Book_Store.Models.Product_Entities
{
    public class ProductCategory
    {
        public int Id { get; set; }
        public string CategoryName { get; set; } = string.Empty;
        public int GoodsQuantity { get; set; }
        public string PhotoLocation { get; set; } = string.Empty;
                
        public List<Book>? Books { get; set; }
    }
}