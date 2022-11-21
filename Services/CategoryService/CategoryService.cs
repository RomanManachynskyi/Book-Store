using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Book_Store.Data;
using Book_Store.Dtos.Product_Entities.Category;
using Book_Store.Models;
using Book_Store.Models.Product_Entities;
using Book_Store.Models.User;
using Microsoft.EntityFrameworkCore;

namespace Book_Store.Services.CategoryService
{
    public class CategoryService : ICategoryService
    {
        private readonly IMapper mapper;
        public readonly DataContext context;

        private readonly IHttpContextAccessor httpContextAccessor;

        public CategoryService(IMapper mapper, DataContext context, IHttpContextAccessor httpContextAccessor)
        {
            this.context = context;
            this.httpContextAccessor = httpContextAccessor;
            this.mapper = mapper;
        }
        private int GetUserId() => Convert.ToInt32(httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));

        public async Task<ServiceResponse<List<GetCategoryDto>>> GetAllCategories()
        {
            var serviceResponse = new ServiceResponse<List<GetCategoryDto>>();
            var dbProducts = await context.Categories.ToListAsync();
            serviceResponse.StatusCode = 200;
            serviceResponse.Data = dbProducts.Select(c => mapper.Map<GetCategoryDto>(c)).ToList();
            return serviceResponse;
        }        

        public async Task<ServiceResponse<List<GetCategoryDto>>> AddCategory(AddCategoryDto newCategory)
        {            
            var serviceResponse = new ServiceResponse<List<GetCategoryDto>>();

            User currentUser = await context.User.FirstAsync(c => c.Id == GetUserId());
            if(currentUser.Role != Role.Seller)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "Permission denied";
                serviceResponse.StatusCode = 403;
                return serviceResponse;
            }            
            ProductCategory category = mapper.Map<ProductCategory>(newCategory);

            context.Categories.Add(category);
            await context.SaveChangesAsync();
            serviceResponse.StatusCode = 200;
            serviceResponse.Data = await context.Categories.Select(c => mapper.Map<GetCategoryDto>(c)).ToListAsync();

            return serviceResponse;
        }

        public async Task<ServiceResponse<GetCategoryDto>> UpdateCategory(UpdateCategoryDto updatedCategory, int id)
        {           
            var serviceResponse = new ServiceResponse<GetCategoryDto>();

            try
            {
                User currentUser = await context.User.FirstAsync(c => c.Id == GetUserId());
                if(currentUser.Role != Role.Seller)
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "Permission denied";
                    serviceResponse.StatusCode = 403;
                    return serviceResponse;
                }                 
                var categoryToUpdate = await context.Categories.FirstAsync(c => c.Id == id);
                mapper.Map(updatedCategory, categoryToUpdate);
                await context.SaveChangesAsync();
                serviceResponse.StatusCode = 200;
                serviceResponse.Data = mapper.Map<GetCategoryDto>(categoryToUpdate);
            }
            catch(Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetCategoryDto>>> DeleteCategory(int id)
        {
            var serviceResponse = new ServiceResponse<List<GetCategoryDto>>();

            try
            {
                User currentUser = await context.User.FirstAsync(c => c.Id == GetUserId());
                if(currentUser.Role != Role.Seller)
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "Permission denied";
                    serviceResponse.StatusCode = 403;
                    return serviceResponse;
                }
                Book book = await context.Book.FirstOrDefaultAsync(c => c.CategoryId == id);
                if(book != null)
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "Cateory is used";
                    return serviceResponse;
                }                
                
                var category = await context.Categories.FirstAsync(c => c.Id == id);

                context.Categories.Remove(category);
                await context.SaveChangesAsync();

                serviceResponse.StatusCode = 200;
                serviceResponse.Data = context.Categories.Select(c => mapper.Map<GetCategoryDto>(c)).ToList();
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