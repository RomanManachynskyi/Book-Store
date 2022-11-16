using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Book_Store.Models.User;

namespace Book_Store.Models.Product_Entities
{
    public class Author
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string SecondName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string BirthDate { get; set; } = string.Empty;
        public string photoLocation { get; set; } = string.Empty;

        public List<Book>? Books { get; set; }
    }
}