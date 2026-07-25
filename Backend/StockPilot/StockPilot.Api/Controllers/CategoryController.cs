using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using StockPilot.Api.RateLimiting;
using StockPilot.Application.Features.Command.Category;
using StockPilot.Application.Features.Queries.Category.ListingCategory;

namespace StockPilot.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : BaseController
    {
        public CategoryController(ISender sender) : base(sender) { }


        [Authorize(Roles = "Admin")]
        [HttpPost("add")]
        [EnableRateLimiting(RateLimitPolicies.General)]
        public async Task<ActionResult> AddAsync([FromBody] AddCategoryCommand request)
        {
            var command = await Sender.Send(request);
            return HandleResponse(command);
        }

        [HttpGet("listing-category")]
        public async Task<ActionResult<List<ListingCategoryQueryDto>>> ListingAsync()
        {
            var query = await Sender.Send(new  ListingCategoryQuery());
            return HandleResponse(query);
        }
    }
}
