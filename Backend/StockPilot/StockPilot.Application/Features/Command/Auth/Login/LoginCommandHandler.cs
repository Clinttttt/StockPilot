using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using StockPilot.Application.Common.Interfaces.Data;
using StockPilot.Application.Common.Interfaces.Services;
using StockPilot.Application.Dtos;
using StockPilot.Domain.Common;
using StockPilot.Domain.Entities.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace StockPilot.Application.Features.Command.Auth.Login
{
    public class LoginCommandHandler(IAppDbContext context, ITokenServices tokenServices) : IRequestHandler<LoginCommand, Result<TokenResponseDto>>
    {
        public async Task<Result<TokenResponseDto>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var user = await context.baseUsers.FirstOrDefaultAsync(s=> s.UserName == request.UserName);
            if (user is null) return Result<TokenResponseDto>.NotFound("User not found");

            if(new PasswordHasher<BaseUser>().VerifyHashedPassword(user, user.PasswordHash!, request.Password!) == PasswordVerificationResult.Failed)
            {
                user.FieldAttempts();
                await context.SaveChangesAsync(cancellationToken);
                return Result<TokenResponseDto>.Unauthorized("Invalid password");
            }
            return Result<TokenResponseDto>.Success(await tokenServices.CreateTokenResponse(user,cancellationToken));

        }
    }
}
