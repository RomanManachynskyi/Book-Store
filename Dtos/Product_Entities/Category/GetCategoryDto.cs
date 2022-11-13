using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Book_Store.Dtos.Product_Entities.Category
{
    public class GetCategoryDto
    {
        public int Id { get; set; }
        public string CategoryName { get; set; } = string.Empty;
        public int GoodsQuantity { get; set; }
        public string PhotoLocation { get; set; } = string.Empty;
    }
}