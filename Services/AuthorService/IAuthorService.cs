using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Book_Store.Models;
using Book_Store.Dtos.Product_Entities.Author;

namespace Book_Store.Services.AuthorService
{
    public interface IAuthorService
    {
        public Task<ServiceResponse<List<GetAuthorDto>>> GetAllAuthors();
        public Task<ServiceResponse<List<GetAuthorDto>>> AddAuthor(AddAuthorDto newAuthor);
        public Task<ServiceResponse<GetAuthorDto>> UpdateAuthor(UpdateAuthorDto updateAuthor, int id);
        public Task<ServiceResponse<List<GetAuthorDto>>> DeleteAuthor(int id);
    }
}