using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Book_Store.Models;
using Book_Store.Dtos.Category;

namespace Book_Store.Services.CategoryService
{
    public interface ICategoryService
    {
        public Task<ServiceResponse<List<GetCategoryDto>>> GetAllCategories();
        public Task<ServiceResponse<List<GetCategoryDto>>> AddCategory(AddCategoryDto product);
        public Task<ServiceResponse<GetCategoryDto>> PatchCategory(PatchCategoryDto patchedCategory, int id);
        public Task<ServiceResponse<List<GetCategoryDto>>> DeleteCategory(int id);
    }
}