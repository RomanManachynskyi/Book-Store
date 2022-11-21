using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
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
        private readonly IHttpContextAccessor httpContextAccessor;

        public OrderService(IMapper mapper, DataContext context, IHttpContextAccessor httpContextAccessor)
        {
            this.context = context;
            this.httpContextAccessor = httpContextAccessor;
            this.mapper = mapper;
        } 
        
        private int GetUserId() => Convert.ToInt32(httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));

        public async Task<ServiceResponse<List<GetOrderDto>>> GetOrders()
        {
            var serviceResponse = new ServiceResponse<List<GetOrderDto>>();
            var dbProducts = await context.Orders.Where(c => c.UserId == GetUserId())
                .Include(c => c.Book).Include(c => c.Book.Author).Include(c => c.Book.Category).ToListAsync();
            serviceResponse.Data = dbProducts.Select(c => mapper.Map<GetOrderDto>(c)).ToList();
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetOrderDto>>> CreateOrder(CreateOrderDto newOrder)
        {
            var serviceResponse = new ServiceResponse<List<GetOrderDto>>();
            Book book = null;          
    
            try
            {
                book = await context.Book.FirstAsync(c => c.Id == newOrder.BookId);
            }
            catch(Exception)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "Book not found";
                return serviceResponse;
            }

            Orders order = mapper.Map<Orders>(newOrder);
            order.UserId = GetUserId();
            order.TransactionDate = Convert.ToString(DateTime.Now);
            context.Orders.Add(order);
            await context.SaveChangesAsync();

            serviceResponse.Data = await context.Orders.Where(c => c.UserId == GetUserId())
                .Include(c => c.Book).Include(c => c.Book.Author).Include(c => c.Book.Category).Select(c => mapper.Map<GetOrderDto>(c)).ToListAsync();


            return serviceResponse;
        }

        public async Task<ServiceResponse<GetOrderDto>> UpdateOrder(UpdateOrderDto updatedOrder, int id)
        {
            var serviceResponse = new ServiceResponse<GetOrderDto>();

            try
            {
                Orders orderToUpdate = await context.Orders.Include(c => c.Book).Include(c => c.Book.Author).Include(c => c.Book.Category).FirstAsync(c => c.Id == id && c.UserId == GetUserId());
                if(orderToUpdate == null)
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "Order not found";
                    return serviceResponse;
                }

                mapper.Map(updatedOrder, orderToUpdate);
                context.SaveChanges();
                serviceResponse.Data = mapper.Map<GetOrderDto>(orderToUpdate);
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
                var order = await context.Orders.FirstAsync(c => c.Id == id && c.UserId == GetUserId());
                context.Orders.Remove(order);
                await context.SaveChangesAsync();

                serviceResponse.Data = await context.Orders.Where(c => c.UserId == GetUserId())
                    .Include(c => c.Book.Category).Include(c => c.Book.Author).Select(c => mapper.Map<GetOrderDto>(c)).ToListAsync();
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