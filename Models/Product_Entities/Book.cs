using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Book_Store.Models.Product_Entities;

namespace Book_Store.Models.Product_Entities
{
    public class Book
    {
        public int Id { get; set; }
        public string BookName { get; set; }
        public double  BookPrice { get; set; }
        public int CategoryId { get; set; }
        public int AuthorId { get; set; }
        public string Publisher { get; set; }
        public string YearOfPublication { get; set; }
        public Language Language { get; set; }
        public Cover Cover { get; set; }
        public int BookWidth { get; set; }
        public int BookLength { get; set; }    
        public BookType BookType { get; set; }
        public string PhotoLocation { get; set; }
        public int ProductQuantity { get; set; }
    }
}