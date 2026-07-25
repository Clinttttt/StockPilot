using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore.Query.Internal;
using StockPilot.Api.RateLimiting;
using StockPilot.Application.Common.Model;
using StockPilot.Application.Features.Command.Product.AddProduct.cs;
using StockPilot.Application.Features.Queries.Product.DeactivateProduct;
using StockPilot.Application.Features.Queries.Product.ListingProduct;
using System.Security.Cryptography.X509Certificates;

namespace StockPilot.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : BaseController
    {
       public ProductController(ISender sender) : base(sender) { }

        [Authorize(Roles = "Admin")]
        [EnableRateLimiting(RateLimitPolicies.General)]
        [HttpPost]
        public async Task<ActionResult> AddAsync([FromBody] AddProductCommand request)
        {
            var command = await Sender.Send(request);
             return HandleResponse(command);
        }


        [HttpGet("list")]
        public async Task<ActionResult<PaginatedList<ListingProductQueryDto>>> ListingAsync([FromQuery] ListingProductQuery command)
        {
            var query = await Sender.Send(command);
            return HandleResponse(query);
        }

        [HttpPatch("deactive-product/{id:guid}")]
        public async Task<ActionResult> DeactivateAsync([FromRoute] Guid id)
        {

            var command = await Sender.Send(new DeactivateProductCommand(id));
            return HandleResponse(command);
        }
    }
}
