using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Book_Store.Dtos.User;
using Book_Store.Models;
using Book_Store.Models.User;
using Book_Store.Services.AuthService;
using Microsoft.AspNetCore.Mvc;

namespace Book_Store.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService authService;

        public AuthController(IAuthService authService)
        {
            this.authService = authService;
        }

        [HttpPost("register")]
        public async Task<ActionResult<ServiceResponse<int>>> Register(UserRegisterDto request)
        {
            var serverResponse = await authService.Register(new User { Username = request.Username, Mail = request.Mail, Role = request.Role }, request.Password);
            if(!serverResponse.Success)
                return BadRequest(serverResponse);

            return Ok(serverResponse);
        }

       [HttpPost("login")]
        public async Task<ActionResult<ServiceResponse<int>>> Login(UserLoginDto request)
        {
            var serverResponse = await authService.Login(request.Username, request.Password);
            if(!serverResponse.Success)
                return BadRequest(serverResponse);

            return Ok(serverResponse);
        }
    }
}