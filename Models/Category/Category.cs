using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Book_Store.Models.Category
{
    public class Category
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public int ProductQuantity { get; set; }
    }
}