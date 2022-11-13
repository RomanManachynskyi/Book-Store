using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Book_Store.Data;
using Book_Store.Dtos.Product_Entities.Category;
using Book_Store.Models;
using Book_Store.Models.Product_Entities;
using Microsoft.EntityFrameworkCore;

namespace Book_Store.Services.CategoryService
{
    public class CategoryService : ICategoryService
    {
        private readonly IMapper mapper;
        public readonly DataContext context;

        public CategoryService(IMapper mapper, DataContext context)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<ServiceResponse<List<GetCategoryDto>>> GetAllCategories()
        {
            var serviceResponse = new ServiceResponse<List<GetCategoryDto>>();
            var dbProducts = await context.Categories.ToListAsync();
            serviceResponse.Data = dbProducts.Select(c => mapper.Map<GetCategoryDto>(c)).ToList();
            return serviceResponse;
        }        

        public async Task<ServiceResponse<List<GetCategoryDto>>> AddCategory(AddCategoryDto newCategory)
        {
            var serviceResponse = new ServiceResponse<List<GetCategoryDto>>();
            ProductCategory category = mapper.Map<ProductCategory>(newCategory);

            context.Categories.Add(category);
            await context.SaveChangesAsync();
            serviceResponse.Data = await context.Categories.Select(c => mapper.Map<GetCategoryDto>(c)).ToListAsync();

            return serviceResponse;
        }

        public async Task<ServiceResponse<GetCategoryDto>> UpdateCategory(UpdateCategoryDto updatedCategory, int id)
        {
            var serviceResponse = new ServiceResponse<GetCategoryDto>();

            try
            {
                var categoryToUpdate = await context.Categories.FirstAsync(c => c.Id == id);
                mapper.Map(updatedCategory, categoryToUpdate);
                await context.SaveChangesAsync();
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
                var category = await context.Categories.FirstAsync(c => c.Id == id);
                context.Categories.Remove(category);
                await context.SaveChangesAsync();

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