using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using StockPilot.Application.Common;
using StockPilot.Application.Common.Interfaces.Services;
using StockPilot.Application.Dtos;
using StockPilot.Domain.Common;
using StockPilot.Domain.Entities.Users;
using StockPilot.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
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
        public async Task<string> GenerateAndSaveRefreshToken(BaseUser user,CancellationToken cancellationToken = default)
        {
            var refreshtoken = GenerateRandomNumber();
            user.RefreshToken = refreshtoken;
            await context.SaveChangesAsync(cancellationToken);
            return refreshtoken;
        }
        public async Task<Result<BaseUser>> ValidateRefreshToken(string refreshToken, CancellationToken cancellationToken = default)
        {
            var hashedRefreshToken = Hasher(refreshToken);
            var findUser = await context.baseUsers.FirstOrDefaultAsync(s => s.RefreshToken == hashedRefreshToken, cancellationToken);
            if (findUser is null || findUser.RefreshTokenExpiryTime < DateTime.Now)
            {
                return Result<BaseUser>.Unauthorized();
            }
            return Result<BaseUser>.Success(findUser);
        }

        public async Task<TokenResponseDto> CreateTokenResponse(BaseUser user,CancellationToken cancellationToken = default)
        {
            return new TokenResponseDto
            {
                AccessToken = CreateToken(user),
                RefreshToken = await GenerateAndSaveRefreshToken(user, cancellationToken)
            };
        }
        public async Task<Result> RevokeRefreshtoken(string refreshToken, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(refreshToken))
            {
                return Result.Failure("Refreshtoken is empty");
            }
            var hash = Hasher(refreshToken);
            var user = await context.baseUsers.FirstOrDefaultAsync(s => s.RefreshToken == hash);

            if (user is null)
            {
                return Result.NotFound("User not found");
            }

            user.ClearRefreshtoken();
            await context.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }
        public static string Hasher(string token)
         => Convert.ToBase64String(SHA256.HashData(Encoding.UTF8.GetBytes(token)));
    }

}
