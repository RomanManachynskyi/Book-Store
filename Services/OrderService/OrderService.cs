using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Book_Store.Data;
using Book_Store.Dtos.Order;
using Book_Store.Models;
using Book_Store.Models.Orders;
using Book_Store.Models.Product_Entities;
using Book_Store.Models.User;
using Microsoft.EntityFrameworkCore;

namespace Book_Store.Services.OrderService
{
    public class OrderService : IOrderService
    {
        private readonly IMapper mapper;
        public readonly DataContext context;

        public OrderService(IMapper mapper, DataContext context)
        {
            this.context = context;
            this.mapper = mapper;
        } 
        

        public async Task<ServiceResponse<List<GetOrderDto>>> GetOrders()
        {
            var serviceResponse = new ServiceResponse<List<GetOrderDto>>();
            var dbProducts = await context.Orders.Include(c => c.Book).Include(c => c.Book.Author).Include(c => c.Book.Category).ToListAsync();
            serviceResponse.Data = dbProducts.Select(c => mapper.Map<GetOrderDto>(c)).ToList();
            return serviceResponse;
        }
        public async Task<ServiceResponse<List<GetOrderDto>>> GetOrderHistory()
        {
            var serviceResponse = new ServiceResponse<List<GetOrderDto>>();
            var dbOrders = await context.OrderHistory.Include(c => c.Book).Include(c => c.Book.Author).Include(c => c.Book.Category).ToListAsync();
            serviceResponse.Data = dbOrders.Select(c => mapper.Map<GetOrderDto>(c)).ToList();
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetOrderDto>>> CreateOrder(CreateOrderDto newOrder)
        {
            var serviceResponse = new ServiceResponse<List<GetOrderDto>>();

            try
            {
                User user = await context.User.FirstOrDefaultAsync(c => c.Id == newOrder.UserId);
                if(user == null)
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "User not found";
                    return serviceResponse;
                }                
                Book book = await context.Book.FirstOrDefaultAsync(c => c.Id == newOrder.BookId);
                if(book == null)
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "Book not found";
                    return serviceResponse;
                }

                Orders order = mapper.Map<Orders>(newOrder);
                context.Orders.Add(order);
                await context.SaveChangesAsync();

                serviceResponse.Data = await context.Orders.Include(c => c.Book).Include(c => c.Book.Author).Include(c => c.Book.Category).Select(c => mapper.Map<GetOrderDto>(c)).ToListAsync();
            }
            catch(Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetOrderDto>>> MoveOrderToorderHistory(int id)
        {
            var serviceResponse = new ServiceResponse<List<GetOrderDto>>();

            try
            {
                var product = await context.Orders.FirstAsync(c => c.Id == id);
                if(product == null)
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "Order not found";
                    return serviceResponse;
                } 

                Orders order = mapper.Map<Orders>(product);
                context.Orders.Remove(order);

                CreateOrderDto orderDto = mapper.Map<CreateOrderDto>(product);
                OrderHistory orderInHistory = mapper.Map<OrderHistory>(orderDto);
                orderInHistory.OrderReceived = true;
                context.OrderHistory.Add(orderInHistory);

                context.SaveChanges();

                var dbProducts = await context.Orders.Include(c => c.Book).Include(c => c.Book.Author).Include(c => c.Book.Category).ToListAsync();
                serviceResponse.Data = dbProducts.Select(c => mapper.Map<GetOrderDto>(c)).ToList();
            }
            catch(Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;
        }
        
        public async Task<ServiceResponse<List<GetOrderDto>>> DeleteOrder(int id)
        {
            var serviceResponse = new ServiceResponse<List<GetOrderDto>>();

            try
            {
                var order = await context.Orders.FirstAsync(c => c.Id == id);
                context.Orders.Remove(order);
                await context.SaveChangesAsync();

                serviceResponse.Data = await context.Orders.Include(c => c.Book.Category).Include(c => c.Book.Author).Select(c => mapper.Map<GetOrderDto>(c)).ToListAsync();
            }
            catch(Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetOrderDto>>> DeleteOrderInHistory(int id)
        {
            var serviceResponse = new ServiceResponse<List<GetOrderDto>>();

            try
            {
                var order = await context.OrderHistory.FirstAsync(c => c.Id == id);
                context.OrderHistory.Remove(order);
                await context.SaveChangesAsync();

                serviceResponse.Data = await context.OrderHistory.Include(c => c.Book.Category).Include(c => c.Book.Author).Select(c => mapper.Map<GetOrderDto>(c)).ToListAsync();
            }
            catch(Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }
    }
}