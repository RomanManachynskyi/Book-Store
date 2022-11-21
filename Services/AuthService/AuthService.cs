using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Book_Store.Data;
using Book_Store.Models;
using Book_Store.Models.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Book_Store.Services.AuthService
{
    public class AuthService : IAuthService
    {
        private readonly DataContext context;
        private readonly IConfiguration configuration;

        public AuthService(DataContext context, IConfiguration configuration)
        {
            this.context = context;
            this.configuration = configuration;
        }

        public async Task<ServiceResponse<string>> Login(string username, string password)
        {
            var serviceResponse = new ServiceResponse<string>();
            User user = null;
            try
            {
                user = await context.User.FirstAsync(u => u.Username.ToLower().Equals(username.ToLower()));
            }
            catch(Exception)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "User not found";
                return serviceResponse;
           }

            if(!VerifyPasswordHash(password, user.PaswordHash, user.PaswordSalt))
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "Incorrect password";
            }
            else
            {
                serviceResponse.Data = CreateToken(user);
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<int>> Register(User user, string password)
        {
            var serviceResponse = new ServiceResponse<int>();

            if(await UserExists(user.Username))
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "User already exists";
            }
            else if(await MailIsUsed(user.Mail))
            {
                 serviceResponse.Success = false;
                 serviceResponse.Message = "Mail is already used";
            }
            else
            {
                CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);
                user.PaswordHash = passwordHash;
                user.PaswordSalt = passwordSalt;

                context.User.Add(user);
                await context.SaveChangesAsync();
                serviceResponse.Data = user.Id;
            }

            return serviceResponse;
        }

        public async Task<bool> UserExists(string username)
        {
            if(await context.User.AnyAsync(u => u.Username.ToLower() == username.ToLower()))
                return true;
            
            return false;
        }

        public async Task<bool> MailIsUsed(string mail)
        {
            if(await context.User.AnyAsync(u => u.Mail.ToLower() == mail.ToLower()))
                return true;
            
            return false;
        }




        public void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computeHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computeHash.SequenceEqual(passwordHash);
            }
        }

        private string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Email, user.Mail),
            };

            SymmetricSecurityKey key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(configuration.GetSection("AppSettings:Token").Value));
            SigningCredentials credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = credentials
            };

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}