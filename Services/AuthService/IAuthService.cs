using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Book_Store.Models;
using Book_Store.Models.User;

namespace Book_Store.Services.AuthService
{
    public interface IAuthService
    {
        Task<ServiceResponse<int>> Register(User user, string password);
        Task<ServiceResponse<string>> Login(string user, string password);
        Task<bool> UserExists(string username);
        Task<bool> MailIsUsed(string mail);
    }
}