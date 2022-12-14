using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Book_Store.Dtos.Product_Entities.Category;
using Book_Store.Models;
using Book_Store.Services.CategoryService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Book_Store.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class CategoryController : Controller
    {
        private readonly ICategoryService categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            this.categoryService = categoryService;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<ServiceResponse<List<GetCategoryDto>>>> GetAllCategories()
        {
            return Ok(await categoryService.GetAllCategories());
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<List<GetCategoryDto>>>> AddCategory(AddCategoryDto newCategory)
        {
            var serverResponse = await categoryService.AddCategory(newCategory);
            if(serverResponse.StatusCode == 403)
                return Forbid();
            return Ok(serverResponse);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ServiceResponse<GetCategoryDto>>> UpdateProduct(UpdateCategoryDto updatedCategory, int id)
        {
            var serverResponse = await categoryService.UpdateCategory(updatedCategory, id);
            if(serverResponse.StatusCode == 403)
                return Forbid();

            switch(serverResponse.Data)
            {
                case null:
                    return NotFound(serverResponse);
            }

            return Ok(serverResponse);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ServiceResponse<GetCategoryDto>>> DeleteCategory(int id)
        {
            var serverResponse = await categoryService.DeleteCategory(id);
            if(serverResponse.StatusCode == 403)
                return Forbid();

            switch(serverResponse.Data)
            {
                case null:
                    return NotFound(serverResponse);
            }

            return Ok(serverResponse);
        } 
    }
}