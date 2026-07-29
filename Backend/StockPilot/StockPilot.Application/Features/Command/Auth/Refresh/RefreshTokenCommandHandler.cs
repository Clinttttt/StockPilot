using MediatR;
using StockPilot.Application.Common.Interfaces.Services;
using StockPilot.Application.Dtos;
using StockPilot.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace StockPilot.Application.Features.Command.Auth.Refresh
{
    internal class RefreshTokenCommandHandler(ITokenServices tokenServices) : IRequestHandler<RefreshTokenCommand, Result<TokenResponseDto>>
    {
        public async Task<Result<TokenResponseDto>> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            var validate = await tokenServices.ValidateRefreshToken(request.RefreshToken, cancellationToken);
            var tokenReponse = await tokenServices.CreateTokenResponse(validate.Value,cancellationToken);
            return Result<TokenResponseDto>.Success(tokenReponse);

        }
    }
}
