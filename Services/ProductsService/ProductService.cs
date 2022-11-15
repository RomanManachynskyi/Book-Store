using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Book_Store.Dtos.Product_Entities.Product;
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
            var dbProducts = await context.Book.Include(c => c.Author).Include(c => c.Category).ToListAsync();
            serviceResponse.Data = dbProducts.Select(c => mapper.Map<GetProductsDto>(c)).ToList();
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetProductsDto>>> AddProduct(AddProductDto newProduct)
        {
            var serviceResponse = new ServiceResponse<List<GetProductsDto>>();

            try
            {
                ProductCategory category = await context.Categories.FirstAsync(c => c.Id == newProduct.CategoryId);
                if(category == null)
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "Category not found";
                    return serviceResponse;
                }
                Author author = await context.Author.FirstAsync(c => c.Id == newProduct.AuthorId);
                if(author == null)
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "Author not found";
                    return serviceResponse;
                }
                Book book = mapper.Map<Book>(newProduct);
                context.Book.Add(book);
                await context.SaveChangesAsync();

                serviceResponse.Data = await context.Book.Select(c => mapper.Map<GetProductsDto>(c)).ToListAsync();
            }
            catch(Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<GetProductsDto>> UpdateProduct(UpdateProductDto updatedProduct, int id)
        {
            var serviceResponse = new ServiceResponse<GetProductsDto>();

            try
            {
                ProductCategory category = await context.Categories.FirstAsync(c => c.Id == updatedProduct.CategoryId);
                if(category == null)
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "Category not found";
                    return serviceResponse;
                }
                Author author = await context.Author.FirstAsync(c => c.Id == updatedProduct.AuthorId);
                if(author == null)
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "Author not found";
                    return serviceResponse;
                }
                var productToUpdate = await context.Book.FirstAsync(c => c.Id == id);
                mapper.Map(updatedProduct, productToUpdate);
                context.SaveChanges();
                serviceResponse.Data = mapper.Map<GetProductsDto>(productToUpdate);
            }
            catch(Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;
        }
        
        public async Task<ServiceResponse<List<GetProductsDto>>> DeleteProduct(int id)
        {
            var serviceResponse = new ServiceResponse<List<GetProductsDto>>();

            try
            {
                var product = await context.Book.FirstAsync(c => c.Id == id);
                context.Book.Remove(product);
                await context.SaveChangesAsync();

                serviceResponse.Data = await context.Book.Include(c => c.Category).Include(c => c.Author).Select(c => mapper.Map<GetProductsDto>(c)).ToListAsync();
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