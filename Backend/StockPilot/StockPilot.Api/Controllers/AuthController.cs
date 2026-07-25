using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using StockPilot.Api.Extension;
using StockPilot.Api.RateLimiting;
using StockPilot.Application.Dtos;
using StockPilot.Application.Features.Command.Auth.Login;
using StockPilot.Application.Features.Command.Auth.Register;

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

        [Authorize]
        [HttpGet("me")]
        public IActionResult Me() => Ok();
    }
}
