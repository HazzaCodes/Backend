using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Dtos;
using Backend.Models;

namespace Backend.UserService
{
    public interface IAuthenticationService
    {
        Task<ServiceResponse<int>> Register(User user, string password);
        Task<ServiceResponse<string>> Login(string username, string password);
        
    }
}