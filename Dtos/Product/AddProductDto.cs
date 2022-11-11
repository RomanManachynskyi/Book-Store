using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Book_Store.Models.Product_Entities;

namespace Book_Store.Dtos.Product
{
    public class AddProductDto
    {
        public string BookName { get; set; }
        public double  BookPrice { get; set; }
        public ProductCategory Category { get; set; }
        public Author Author { get; set; }
        public string Publisher { get; set; }
        public string yearOfPublication { get; set; }
        public Language Language { get; set; }
        public Cover Cover { get; set; }
        public int BookWidth { get; set; }
        public int BookLength { get; set; } 
        public BookType BookType { get; set; }
        public string photoLocation { get; set; }
        public int ProductQuantity { get; set; }
    }
}