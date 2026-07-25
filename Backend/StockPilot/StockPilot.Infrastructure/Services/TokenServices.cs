using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using StockPilot.Application.Common;
using StockPilot.Application.Common.Interfaces.Services;
using StockPilot.Application.Dtos;
using StockPilot.Domain.Entities.Users;
using StockPilot.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using static StockPilot.Domain.Entities.Enums;

namespace StockPilot.Infrastructure.Services
{
    public class TokenServices(AppDbContext context, IConfiguration configuration) : ITokenServices
    {
        public string CreateToken(BaseUser user)
        {
            var claim = new List<Claim>
            {
                new Claim(AppClaimTypes.UserId, user.Id.ToString()),
                new Claim(AppClaimTypes.UserName, user.FullName),
                new Claim(AppClaimTypes.UserEmail, user.Email ?? string.Empty),
                new Claim(AppClaimTypes.UserRole, user.userRole.ToString())
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtSettings:SecretKey"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                audience: configuration["JwtSettings:Audience"],
                issuer: configuration["JwtSettings:Issuer"],
                claims: claim,
                expires: DateTime.Now.AddDays(7),
                signingCredentials: creds
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public string GenerateRandomNumber()
        {
            var random = new byte[32];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(random);
            return Convert.ToBase64String(random);
        }
        public async Task<string> GenerateAndSaveRefreshToken(BaseUser user)
        {
            var refreshtoken = GenerateRandomNumber();
            user.RefreshToken = refreshtoken;
            await context.SaveChangesAsync();
            return refreshtoken;
        }
        public async Task<bool> ValidateRefreshToken(BaseUser user, string refreshToken)
        {
            var findUser = await context.baseUsers.FirstOrDefaultAsync(s => s.RefreshToken == refreshToken);
            if (findUser is null || user.RefreshTokenExpiryTime < DateTime.Now)
            {
                return false;
            }
            return true;
        }
 
        public async Task<TokenResponseDto> CreateTokenResponse(BaseUser user)
        {
            return new TokenResponseDto
            {
                AccessToken = CreateToken(user),
                RefreshToken = await GenerateAndSaveRefreshToken(user)
            };
        }
    }

}
