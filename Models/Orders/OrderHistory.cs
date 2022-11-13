using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Book_Store.Models.Orders
{
    public class OrderHistory
    {
        public int Id { get; set; }
        public Models.User.User User { get; set; }

        [ForeignKey("Book")]
        public int BookId { get; set; }
         
        public int TransactionNumber { get; set; }
        public string TransactionDate { get; set; } = string.Empty;
        public bool OrderReceived { get; set; } = false;

        public Product_Entities.Book Book { get; set; }
    }
}