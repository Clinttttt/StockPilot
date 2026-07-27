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
using StockPilot.Application.Features.Command.Product.AdjustStock;
using StockPilot.Application.Features.Queries.Product.DeactivateProduct;
using StockPilot.Application.Features.Queries.Product.GetInventorySummary;
using StockPilot.Application.Features.Queries.Product.GetLowStock;
using StockPilot.Application.Features.Queries.Product.ListingProduct;
using System.Formats.Asn1;
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
        [HttpPost("add")]
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

        [HttpPatch("deactivate/{id:guid}")]
        public async Task<ActionResult> DeactivateAsync([FromRoute] Guid id)
        {

            var command = await Sender.Send(new DeactivateProductCommand(id));
            return HandleResponse(command);
        }

        [HttpPatch("adjust-stock")]
        public async Task<ActionResult> AdjustStock([FromQuery] AdjustStockCommand command)
        {
            var result = await Sender.Send(command);
            return HandleResponse(result);
        }

        [HttpGet("low-stock")]
        public async Task<ActionResult<PaginatedList<LowStockProductDto>>> GetLowStock([FromQuery] GetLowStockQuery query)
        {
            var result = await Sender.Send(query);
            return HandleResponse(result);
        }

        [HttpGet("inventory-summary")]
        public async Task<ActionResult<InventorySummaryDto>> GetInventorySummary()
        {
            var result = await Sender.Send(new GetInventorySummaryQuery());
            return HandleResponse(result);
        }
    }
}
