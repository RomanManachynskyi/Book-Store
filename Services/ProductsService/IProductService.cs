using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Book_Store.Models;
using Book_Store.Dtos.Product;

namespace Book_Store.Services.ProductsService
{
    public interface IProductService
    {
        public Task<ServiceResponse<List<GetProductsDto>>> GetAllProducts();
        public Task<ServiceResponse<List<GetProductsDto>>> AddProduct(AddProductDto newProduct);
        public Task<ServiceResponse<GetProductsDto>> UpdateProduct(UpdateProductDto updateProduct, int id);
        public Task<ServiceResponse<List<GetProductsDto>>> DeleteProducts(int id);

    }
}