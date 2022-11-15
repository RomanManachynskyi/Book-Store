using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Book_Store.Dtos.Bucket;
using Book_Store.Models;
using Book_Store.Services.BucketService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace Book_Store.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class BucketController : Controller
    {
        private readonly IBucketService bucketService;
        public BucketController(IBucketService bucketService)
        {
            this.bucketService = bucketService;
        }

        [HttpGet]
        public async Task<ActionResult<ServiceResponse<List<GetBucketItemsDto>>>> GetOrders()
        {
            return Ok(await bucketService.GetBucketItems());
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<List<GetBucketItemsDto>>>> CreateOrder(AddBucketItemDto newBucketItem)
        {
            return Ok(await bucketService.AddBucketItem(newBucketItem));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ServiceResponse<GetBucketItemsDto>>> DeleteProduct(int id)
        {
            var serverResponse = await bucketService.DeleteBucketItem(id);
            switch(serverResponse.Data)
            {
                case null:
                    return NotFound(serverResponse);
            }

            return Ok(serverResponse);
        } 
    }
}