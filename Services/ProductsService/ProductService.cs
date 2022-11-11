using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Book_Store.Dtos.Product;
using Book_Store.Models;
using Book_Store.Models.Product_Entities;
using Book_Store.Data;

namespace Book_Store.Services.ProductsService
{
    public class ProductService : IProductService
    {
        private readonly IMapper mapper;
        public readonly DataContext context;

        public ProductService(IMapper mapper, DataContext context)
        {
            this.context = context;
            this.mapper = mapper;
        } 


        public async Task<ServiceResponse<List<GetProductsDto>>> GetAllProducts()
        {
            var serviceResponse = new ServiceResponse<List<GetProductsDto>>();
            var dbProducts = await context.Book.ToListAsync();
            serviceResponse.Data = dbProducts.Select(c => mapper.Map<GetProductsDto>(c)).ToList();

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetProductsDto>>> AddProduct(AddProductDto newProduct)
        {
            var serviceResponse = new ServiceResponse<List<GetProductsDto>>();
            Book book = mapper.Map<Book>(newProduct);
            await context.SaveChangesAsync();
            serviceResponse.Data = await context.Book.Select(c => mapper.Map<GetProductsDto>(c)).ToListAsync();

            return serviceResponse;
        }

        public async Task<ServiceResponse<GetProductsDto>> UpdateProduct(UpdateProductDto updatedProduct, int id)
        {
            var serviceResponse = new ServiceResponse<GetProductsDto>();

            try
            {
                var productToUpdate = await context.Book.FirstAsync(c => c.Id == id);
                mapper.Map(updatedProduct, productToUpdate);
                await context.SaveChangesAsync();
                serviceResponse.Data = mapper.Map<GetProductsDto>(productToUpdate);
            }
            catch(Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;
        }
        
        public async Task<ServiceResponse<List<GetProductsDto>>> DeleteProducts(int id)
        {
            var serviceResponse = new ServiceResponse<List<GetProductsDto>>();

            try
            {
                var product = await context.Book.FirstAsync(c => c.Id == id);
                context.Book.Remove(product);
                await context.SaveChangesAsync();

                serviceResponse.Data = context.Book.Select(c => mapper.Map<GetProductsDto>(c)).ToList();
            }
            catch(Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            throw new NotImplementedException();
        }
    }
}