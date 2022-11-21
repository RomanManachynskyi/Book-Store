using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Book_Store.Data;
using Book_Store.Dtos.Product_Entities.Author;
using Book_Store.Models;
using Book_Store.Models.Product_Entities;
using Book_Store.Models.User;
using Microsoft.EntityFrameworkCore;

namespace Book_Store.Services.AuthorService
{
    public class AuthorService : IAuthorService
    {
        private readonly IMapper mapper;
        public readonly DataContext context;
        private readonly IHttpContextAccessor httpContextAccessor;

        public AuthorService(IMapper mapper, DataContext context, IHttpContextAccessor httpContextAccessor)
        {
            this.context = context;
            this.httpContextAccessor = httpContextAccessor;
            this.mapper = mapper;
        } 
        private int GetUserId() => Convert.ToInt32(httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));

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
            User currentUser = await context.User.FirstAsync(c => c.Id == GetUserId());
            if(currentUser.Role != Role.Seller)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "Permission denied";
                serviceResponse.StatusCode = 403;
                return serviceResponse;
            }

            Author author = mapper.Map<Author>(newAuthor);

            context.Author.Add(author);
            await context.SaveChangesAsync();
            serviceResponse.StatusCode = 200;
            serviceResponse.Data = await context.Author.Select(c => mapper.Map<GetAuthorDto>(c)).ToListAsync();

            return serviceResponse;
        }

        public async Task<ServiceResponse<GetAuthorDto>> UpdateAuthor(UpdateAuthorDto updatedAuthor, int id)
        {
            var serviceResponse = new ServiceResponse<GetAuthorDto>();

            try
            {
                User currentUser = await context.User.FirstAsync(c => c.Id == GetUserId());
                if(currentUser.Role != Role.Seller)
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "Permission denied";
                    serviceResponse.StatusCode = 403;
                    return serviceResponse;
                }

                var authorToUpdate = await context.Author.FirstAsync(c => c.Id == id);
                mapper.Map(updatedAuthor, authorToUpdate);
                await context.SaveChangesAsync();
                serviceResponse.StatusCode = 200;
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
                User currentUser = await context.User.FirstAsync(c => c.Id == GetUserId());
                if(currentUser.Role != Role.Seller)
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "Permission denied";
                    serviceResponse.StatusCode = 403;
                    return serviceResponse;
                }
                Book book = await context.Book.FirstOrDefaultAsync(c => c.AuthorId == id);
                if(book != null)
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "Author is used";
                    return serviceResponse;
                }

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