using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Book_Store.Data;
using Book_Store.Dtos.Product_Entities.Author;
using Book_Store.Models;
using Book_Store.Models.Product_Entities;
using Microsoft.EntityFrameworkCore;

namespace Book_Store.Services.AuthorService
{
    public class AuthorService : IAuthorService
    {
        private readonly IMapper mapper;
        public readonly DataContext context;

        public AuthorService(IMapper mapper, DataContext context)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<ServiceResponse<List<GetAuthorDto>>> GetAllAuthors()
        {
            var serviceResponse = new ServiceResponse<List<GetAuthorDto>>();
            var dbProducts = await context.Author.ToListAsync();
            serviceResponse.Data = dbProducts.Select(c => mapper.Map<GetAuthorDto>(c)).ToList();
            return serviceResponse;
        }        

        public async Task<ServiceResponse<List<GetAuthorDto>>> AddAuthor(AddAuthorDto newAuthor)
        {
            var serviceResponse = new ServiceResponse<List<GetAuthorDto>>();
            Author author = mapper.Map<Author>(newAuthor);

            context.Author.Add(author);
            await context.SaveChangesAsync();
            serviceResponse.Data = await context.Author.Select(c => mapper.Map<GetAuthorDto>(c)).ToListAsync();

            return serviceResponse;
        }

        public async Task<ServiceResponse<GetAuthorDto>> UpdateAuthor(UpdateAuthorDto updatedAuthor, int id)
        {
            var serviceResponse = new ServiceResponse<GetAuthorDto>();

            try
            {
                var authorToUpdate = await context.Author.FirstAsync(c => c.Id == id);
                mapper.Map(updatedAuthor, authorToUpdate);
                await context.SaveChangesAsync();
                serviceResponse.Data = mapper.Map<GetAuthorDto>(authorToUpdate);
            }
            catch(Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetAuthorDto>>> DeleteAuthor(int id)
        {
            var serviceResponse = new ServiceResponse<List<GetAuthorDto>>();

            try
            {
                var author = await context.Author.FirstAsync(c => c.Id == id);
                context.Author.Remove(author);
                await context.SaveChangesAsync();

                serviceResponse.Data = context.Author.Select(c => mapper.Map<GetAuthorDto>(c)).ToList();
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