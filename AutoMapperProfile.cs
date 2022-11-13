using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Book_Store.Models.Product_Entities;
using Book_Store.Dtos.Product_Entities.Product;
using Book_Store.Dtos.Product_Entities.Author;
using Book_Store.Dtos.Product_Entities.Category;

namespace Book_Store
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Book, GetProductsDto>();
            CreateMap<GetProductsDto, Book>();
            CreateMap<AddProductDto, Book>();
            CreateMap<UpdateProductDto, Book>();

            CreateMap<Author, GetAuthorDto>();
            CreateMap<AddAuthorDto, Author>();
            CreateMap<UpdateAuthorDto, Author>();
            
            CreateMap<ProductCategory, GetCategoryDto>();
            CreateMap<AddCategoryDto, ProductCategory>();
            CreateMap<UpdateCategoryDto, ProductCategory>();                      
        }
    }
}