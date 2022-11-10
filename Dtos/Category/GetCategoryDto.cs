using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Book_Store.Dtos.Category
{
    public class GetCategoryDto
    {
        public string CategoryName { get; set; }
        public int ProductQuantity { get; set; }
    }
}