using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Book_Store.Dtos.Order
{
    public class CreateOrderDto
    {
        public int BookId  { get; set; }        
        public int TransactionNumber { get; set; }
    }
}