using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Book_Store.Dtos.Product_Entities.Author;
using Book_Store.Models;
using Book_Store.Services.AuthorService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Book_Store.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class AuthorController : Controller
    {
        private readonly IAuthorService authorService;
        public AuthorController(IAuthorService authorService)
        {
            this.authorService = authorService;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<ServiceResponse<List<GetAuthorDto>>>> GetAllAuthors()
        {
            return Ok(await authorService.GetAllAuthors());
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<List<GetAuthorDto>>>> AddAuthor(AddAuthorDto newAuthor)
        {
            var serverResponse = await authorService.AddAuthor(newAuthor);
            if(serverResponse.StatusCode == 403)
                return Forbid();
                
            return Ok(serverResponse);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ServiceResponse<GetAuthorDto>>> UpdateAuthor(UpdateAuthorDto updatedAuthor, int id)
        {
            var serverResponse = await authorService.UpdateAuthor(updatedAuthor, id);
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
        public async Task<ActionResult<ServiceResponse<GetAuthorDto>>> DeleteAuthor(int id)
        {
            var serverResponse = await authorService.DeleteAuthor(id);
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