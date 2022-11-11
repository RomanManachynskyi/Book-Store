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
        public Task<ServiceResponse<List<GetOrdetDto>>> GetAllOrders();
        public Task<ServiceResponse<List<GetOrdetDto>>> GetOrderHistory();
        public Task<ServiceResponse<List<GetOrdetDto>>> CreateOrder(CreateOrderDtos createdOrder);
        public Task<ServiceResponse<List<GetOrdetDto>>> DeleteOrder(int id);
    }
}