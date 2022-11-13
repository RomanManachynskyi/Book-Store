using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Book_Store.Dtos.Order
{
    public class GetOrderHistoryDtos
    {
        public int Id { get; set; }
        public Models.Product_Entities.Book Book { get; set; }
        public int TransactionNumber { get; set; }
        public string TransactionDate { get; set; }
    }
}