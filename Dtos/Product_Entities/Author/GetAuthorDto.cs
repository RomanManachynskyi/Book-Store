using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Book_Store.Dtos.Product_Entities.Author
{
    public class GetAuthorDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string SecondName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string BirthDate { get; set; } = string.Empty;
        public string photoLocation { get; set; } = string.Empty;
    }
}