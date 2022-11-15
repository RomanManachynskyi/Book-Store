using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Book_Store.Models;
using Book_Store.Dtos.Bucket;

namespace Book_Store.Services.BucketService
{
    public interface IBucketService
    {
        public Task<ServiceResponse<List<GetBucketItemsDto>>> GetBucketItems();
        public Task<ServiceResponse<List<GetBucketItemsDto>>> AddBucketItem(AddBucketItemDto newBucketItem);
        public Task<ServiceResponse<List<GetBucketItemsDto>>> DeleteBucketItem(int id);
    }
}