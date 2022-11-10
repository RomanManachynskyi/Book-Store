using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Book_Store.Models.Orders
{
    public class OrderHistory
    {
        public int Id { get; set; }
        public int AccountId { get; set; }
        public Product_Entities.Book Book { get; set; }
        public int TransactionNumber { get; set; }
        public string TransactionDate { get; set; }
    }
}