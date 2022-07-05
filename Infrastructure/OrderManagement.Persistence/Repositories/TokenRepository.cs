using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using OrderManagement.Application.DTO.ApiUsers;
using OrderManagement.Application.Repositories;
using OrderManagement.Application.Results;
using OrderManagement.Domain.Entities;
using OrderManagement.Persistence.Contexts;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace OrderManagement.Persistence.Repositories
{
    public class TokenRepository : ITokenRepository
    {
        public IConfiguration _configuration;
        private readonly OrderManagementDbContext _context;

        public TokenRepository(IConfiguration configuration, OrderManagementDbContext context)
        {
            _configuration = configuration;
            _context = context;
        }

        public async Task<string> GetToken(ApiUserModel model)
        {
            if (model != null && model.Email != null && model.Password != null)
            {
                var user = await GetUser(model.Email, model.Password);

                if (user != null)
                {
                    var claims = new[] {
                        new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                        new Claim("Id", user.Id.ToString()),
                        new Claim("Email", user.Email)
                    };

                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                    var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                    var token = new JwtSecurityToken(
                        _configuration["Jwt:Issuer"],
                        _configuration["Jwt:Audience"],
                        claims,
                        expires: DateTime.UtcNow.AddMinutes(10),
                        signingCredentials: signIn);

                    return new JwtSecurityTokenHandler().WriteToken(token);
                }
                else
                {
                    return new ErrorResult("Invalid credentials").Message;
                }
            }
            else
            {
                return new ErrorResult("Email or Password cannot be null!").Message;
            }
        }

        private async Task<ApiUser> GetUser(string email, string password)
        {
            return await _context.ApiUsers.FirstOrDefaultAsync(u => u.Email == email && u.Password == password);
        }
    }
}