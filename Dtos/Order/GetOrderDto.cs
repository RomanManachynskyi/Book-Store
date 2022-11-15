using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Book_Store.Dtos.Product_Entities.Product;

namespace Book_Store.Dtos.Order
{
    public class GetOrderDto
    {
        public int Id { get; set; }
        public GetProductsDto Book { get; set; }
        public int TransactionNumber { get; set; }
        public string TransactionDate { get; set; }
        public bool OrderReceived { get; set; }
    }
}