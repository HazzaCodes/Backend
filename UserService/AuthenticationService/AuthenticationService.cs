using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Security.Principal;
using System.Threading.Tasks;
using Azure;
using Backend.Dtos;
using Backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Backend.UserService
{
    public class AuthenticationService : IAuthenticationService
    {
        private DataContext context;
        private IMapper mapper;

        private IConfiguration configuration;
        public AuthenticationService(DataContext context, IConfiguration configuration, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
            this.configuration = configuration;

        }

        public async Task<ServiceResponse<string>> Login(string username, string password)
        {
            var serviceResponse = new ServiceResponse<string>();
            var user = await context.Users.FirstOrDefaultAsync(user => user.Username.ToLower().Equals(username.ToLower()));
            if (user is null)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "User " + username + " Doesn't exist";
            }
            else if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "Wrong password";
            }
            else
            {
                serviceResponse.Data = CreateToken(user);
            }
            return serviceResponse;

        }

        public Boolean VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt);
            var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            return computedHash.SequenceEqual(passwordHash);

        }
        [HttpPost("Regsiter")]
        public async Task<ServiceResponse<int>> Register(User user, string password)
        {
            var serviceResponse = new ServiceResponse<int>();
            if (await UserExists(user.Username))
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "User " + user.Username + " already exists";
                return serviceResponse;
            }
            CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
            context.Users.Add(user);
            await context.SaveChangesAsync();
            serviceResponse.Data = user.Id;
            return serviceResponse;
        }

        public async Task<bool> UserExists(string username)
        {
            return await context.Users.AnyAsync(u => u.Username.ToLower() == username.ToLower());

        }
        public void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using var hmac = new System.Security.Cryptography.HMACSHA512();
            passwordSalt = hmac.Key;
            passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        }

        private string CreateToken(User user)
        {
            var claims = new List<Claim> {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.Username)
        };
            var appSettingsToken = configuration.GetSection("AppSetting:Token").Value;
            if (appSettingsToken is null)
            {
                throw new Exception("Appsettings token is null");
            }
            SymmetricSecurityKey key = new
             SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(appSettingsToken));

            SigningCredentials creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Today.AddDays(1),
                SigningCredentials = creds
            };

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}