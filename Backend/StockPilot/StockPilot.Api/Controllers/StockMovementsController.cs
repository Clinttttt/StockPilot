using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StockPilot.Application.Features.Queries.MovementStock;
using static StockPilot.Domain.Entities.StockMovement;

namespace StockPilot.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StockMovementsController : BaseController
    {
        public StockMovementsController(ISender sender) : base(sender) { }

        [HttpGet("listing/{id}")]
        public async Task<ActionResult<IReadOnlyList<StockMovementsDto>>> ListingAsync([FromRoute] Guid id, [FromQuery] StockMovementType type)
        {
            var initialize = new GetStockMovementsQuery(id, type);
            var query = await Sender.Send(initialize);
            return HandleResponse(query);
        }
        
    }
}
