using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using StockPilot.Api.Extension;
using StockPilot.Api.RateLimiting;
using StockPilot.Application.Dtos;
using StockPilot.Application.Features.Command.Auth.Login;
using StockPilot.Application.Features.Command.Auth.Logout;
using StockPilot.Application.Features.Command.Auth.Refresh;
using StockPilot.Application.Features.Command.Auth.Register;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace StockPilot.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : BaseController
    {
        public AuthController(ISender sender) : base(sender) { }

        [HttpPost("register")]
        public async Task<ActionResult<bool>> AddAsync([FromBody] RegisterCommand request)
        {
            var command = await Sender.Send(request);
            return HandleResponse(command);
        }

        [HttpPost("login")]
        [EnableRateLimiting(RateLimitPolicies.Login)]
        public async Task<ActionResult<TokenResponseDto>> LoginAsync([FromBody] LoginCommand request)
        {
            var command = await Sender.Send(request);

            if (command.IsSuccess)
            {
                CookieExtension.SetAuthCookies(Response, command.Value.AccessToken!, command.Value.RefreshToken!);
            }

            return HandleResponse(command);
        }

        [HttpPost("logout")]
        public async Task<ActionResult> Logout()
        {

            var refreshTokens = CookieExtension.GetRefreshTokenFromCookie(Request);      
            var result = await Sender.Send(new LogoutCommand(refreshToken: refreshTokens.Value));
            return HandleResponse(result);
        }

        [HttpPost("refresh")]
        public async Task<ActionResult> RefreshAsync()
        {
            var refreshTokens = CookieExtension.GetRefreshTokenFromCookie(Request);
            var result = await Sender.Send(new RefreshTokenCommand(RefreshToken: refreshTokens.Value));
            if (result.IsSuccess)
                CookieExtension.SetAuthCookies(Response, result.Value.AccessToken!, result.Value.RefreshToken!);
            return HandleResponse(result);
        }

        [Authorize]
        [HttpGet("me")]
        public IActionResult Me() => Ok();
    }
}
