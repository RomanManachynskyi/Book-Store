using Book_Store.Dtos.Product_Entities;
using Book_Store.Models;
using Book_Store.Services.ProductsService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Book_Store.Dtos.Product_Entities.Product;

namespace Book_Store.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService productService;
        public ProductController(IProductService productService)
        {
            this.productService = productService;
        }

        [HttpGet]
        public async Task<ActionResult<ServiceResponse<List<GetProductsDto>>>> GetAllProducts()
        {
            return Ok(await productService.GetAllProducts());
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<List<GetProductsDto>>>> AddProduct(AddProductDto newProduct)
        {
            return Ok(await productService.AddProduct(newProduct));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ServiceResponse<GetProductsDto>>> UpdateProduct(UpdateProductDto updatedProduct, int id)
        {
            var serverResponse = await productService.UpdateProduct(updatedProduct, id);
            switch(serverResponse.Data)
            {
                case null:
                    return NotFound(serverResponse);
            }

            return Ok(serverResponse);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ServiceResponse<GetProductsDto>>> DeleteProduct(int id)
        {
            var serverResponse = await productService.DeleteProduct(id);
            switch(serverResponse.Data)
            {
                case null:
                    return NotFound(serverResponse);
            }

            return Ok(serverResponse);
        } 
    }
}