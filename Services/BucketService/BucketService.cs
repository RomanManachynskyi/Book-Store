using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Book_Store.Data;
using Book_Store.Dtos.Bucket;
using Book_Store.Models;
using Book_Store.Models.Bucket;
using Book_Store.Models.Product_Entities;
using Book_Store.Models.User;
using Microsoft.EntityFrameworkCore;

namespace Book_Store.Services.BucketService
{
    public class BucketService : IBucketService
    {
        private readonly IMapper mapper;
        public readonly DataContext context;
        public IHttpContextAccessor httpContextAccessor;

        public BucketService(IMapper mapper, DataContext context, IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
            this.context = context;
            this.mapper = mapper;
        }

        private int GetUserId() => Convert.ToInt32(httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));

        public async Task<ServiceResponse<List<GetBucketItemsDto>>> GetBucketItems()
        {
            var serviceResponse = new ServiceResponse<List<GetBucketItemsDto>>();
            var dbProducts = await context.Bucket.Where(c => c.UserId == GetUserId()).Include(c => c.Book).Include(c => c.Book.Author).Include(c => c.Book.Category).ToListAsync();
            serviceResponse.Data = dbProducts.Select(c => mapper.Map<GetBucketItemsDto>(c)).ToList();
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetBucketItemsDto>>> AddBucketItem(AddBucketItemDto newBucketItem)
        {
            var serviceResponse = new ServiceResponse<List<GetBucketItemsDto>>();

            try
            {
                User user = await context.User.FirstOrDefaultAsync(c => c.Id == GetUserId());//newBucketItem.UserId);
                if(user == null)
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "User not found";
                    return serviceResponse;
                }                
                Book book = await context.Book.FirstOrDefaultAsync(c => c.Id == newBucketItem.BookId);
                if(book == null)
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "Book not found";
                    return serviceResponse;
                }

                Bucket bucket = mapper.Map<Bucket>(newBucketItem);
                bucket.UserId = GetUserId();
                context.Bucket.Add(bucket);
                await context.SaveChangesAsync();

                serviceResponse.Data = await context.Bucket.Where(c => c.UserId == GetUserId())
                    .Include(c => c.Book).Include(c => c.Book.Author).Include(c => c.Book.Category).Select(c => mapper.Map<GetBucketItemsDto>(c)).ToListAsync();
            }
            catch(Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetBucketItemsDto>>> DeleteBucketItem(int id)
        {
            var serviceResponse = new ServiceResponse<List<GetBucketItemsDto>>();

            try
            {
                var bucket = await context.Bucket.FirstAsync(c => c.Id == id && c.UserId == GetUserId());
                if(bucket != null)
                {
                    context.Bucket.Remove(bucket);
                    await context.SaveChangesAsync();

                    serviceResponse.Data = await context.Bucket.Where(c => c.UserId == GetUserId())
                        .Include(c => c.Book.Category).Include(c => c.Book.Author).Select(c => mapper.Map<GetBucketItemsDto>(c)).ToListAsync();
                }
                else
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "Bucket item not found";
                }

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