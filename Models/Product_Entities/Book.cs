using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace Book_Store.Models.Product_Entities
{
    public class Book
    {
        public int Id { get; set; }
        public string BookName { get; set; } = string.Empty;
        public double  BookPrice { get; set; }
        public PriceCurrensy PriceCurrensy { get; set; }

        public int CategoryId { get; set; }
        public ProductCategory Category { get; set; }

        public int AuthorId { get; set; }    
        public Author Author { get; set; }
        
        public string Publisher { get; set; } = string.Empty;
        public string YearOfPublication { get; set; } = string.Empty;
        public Language Language { get; set; }
        public Cover Cover { get; set; }
        public int BookWidth { get; set; }
        public int BookLength { get; set; }    
        public BookType BookType { get; set; }
        public string PhotoLocation { get; set; } = string.Empty;
        public int ProductQuantity { get; set; }
    }
}