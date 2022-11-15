using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Book_Store.Models;
using Book_Store.Dtos.Order;

namespace Book_Store.Services.OrderService
{
    public interface IOrderService
    {
        public Task<ServiceResponse<List<GetOrderDto>>> GetOrders();
        public Task<ServiceResponse<List<GetOrderDto>>> GetOrderHistory();
        public Task<ServiceResponse<List<GetOrderDto>>> CreateOrder(CreateOrderDto newOrder);
        public Task<ServiceResponse<List<GetOrderDto>>> MoveOrderToorderHistory(int id);
        public Task<ServiceResponse<List<GetOrderDto>>> DeleteOrder(int id);
        public Task<ServiceResponse<List<GetOrderDto>>> DeleteOrderInHistory(int id);
    }
}