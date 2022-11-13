using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Book_Store.Models;
using Book_Store.Dtos.Product_Entities.Category;

namespace Book_Store.Services.CategoryService
{
    public interface ICategoryService
    {
        public Task<ServiceResponse<List<GetCategoryDto>>> GetAllCategories();
        public Task<ServiceResponse<List<GetCategoryDto>>> AddCategory(AddCategoryDto category);
        public Task<ServiceResponse<GetCategoryDto>> UpdateCategory(UpdateCategoryDto patchedCategory, int id);
        public Task<ServiceResponse<List<GetCategoryDto>>> DeleteCategory(int id);
    }
}