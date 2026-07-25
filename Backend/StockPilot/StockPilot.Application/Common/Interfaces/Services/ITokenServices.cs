using StockPilot.Application.Dtos;
using StockPilot.Domain.Entities.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace StockPilot.Application.Common.Interfaces.Services
{
    public interface ITokenServices
    {
        Task<TokenResponseDto> CreateTokenResponse(BaseUser user);

    }
}
