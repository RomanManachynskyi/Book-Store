using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Book_Store.Dtos.Order;
using Book_Store.Models;
using Book_Store.Services.OrderService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;

namespace Book_Store.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class OrdersController : Controller
    {
        private readonly IOrderService orderService;
        public OrdersController(IOrderService orderService)
        {
            this.orderService = orderService;
        }

        [HttpGet]
        public async Task<ActionResult<ServiceResponse<List<GetOrderDto>>>> GetOrders()
        {
            return Ok(await orderService.GetOrders());
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<List<GetOrderDto>>>> CreateOrder(CreateOrderDto newOrder)
        {
            return Ok(await orderService.CreateOrder(newOrder));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ServiceResponse<List<GetOrderDto>>>> MoveOrderToorderHistory(UpdateOrderDto updatedorder, int id)
        {
            return Ok(await orderService.UpdateOrder(updatedorder, id));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ServiceResponse<GetOrderDto>>> DeleteOrder(int id)
        {
            var serverResponse = await orderService.DeleteOrder(id);
            switch(serverResponse.Data)
            {
                case null:
                    return NotFound(serverResponse);
            }

            return Ok(serverResponse);
        }         
    }
}