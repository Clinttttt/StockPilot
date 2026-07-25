using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using StockPilot.Application.Common.Interfaces.Data;
using StockPilot.Domain.Common;
using StockPilot.Domain.Entities.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace StockPilot.Application.Features.Command.Auth.Register
{
    public class RegisterCommandHandler(IAppDbContext context) : IRequestHandler<RegisterCommand, Result<bool>>
    {
        public async Task<Result<bool>> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            if (await context.baseUsers.AnyAsync(s => s.UserName == request.UserName))
            {
                return Result<bool>.Conflict("UserName already exists");
            }

            var passwordHash = new PasswordHasher<string>()
                .HashPassword(null!, request.PasswordHash);

            var user = BaseUser.CreateUser
               (request.FullName,
                request.PhoneNumber,
                request.Email,
                request.Address,
                request.UserName,
                passwordHash,
                request.role);
            context.baseUsers.Add(user);
            await context.SaveChangesAsync(cancellationToken);
            return Result<bool>.Success(true);
        }
    }
}
