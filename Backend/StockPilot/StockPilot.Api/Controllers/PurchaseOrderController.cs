using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Query.Internal;
using StockPilot.Application.Common.Model;
using StockPilot.Application.Features.Command.PurchaseOrder.ReceivePurchaseOrder;
using StockPilot.Application.Features.Command.PurchaseOrder.SubmitPurchaseOrder;
using StockPilot.Application.Features.Command.PurchaseOrder.UpdatePurchaseOrder;
using StockPilot.Application.Features.Queries.Product.ListingProduct;
using StockPilot.Application.Features.Queries.PurchaseOrder.ListingPurchaseOrders;
using StockPilot.Domain.Entities;

namespace StockPilot.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PurchaseOrderController : BaseController
    {
        public PurchaseOrderController(ISender sender) : base(sender) { }


        [HttpPatch("{id}")]
        public async Task<ActionResult> Submit([FromRoute] Guid id, [FromQuery] PurchaseOrderStatus Status)
        {
            var query = new SubmitPurchaseOrderCommand(id, Status);
            var result = await Sender.Send(query);
            return HandleResponse(result);
        }

        [HttpPatch("update")]
        public async Task<ActionResult> Update([FromBody] UpdatePurchaseOrderCommand command)
        {
            var result = await Sender.Send(command);
            return HandleResponse(result);
        }

        [HttpPatch("receive")]
        public async Task<ActionResult> Receive([FromBody] ReceivePurchaseOrderCommand command)
        {
            var result = await Sender.Send(command);
            return HandleResponse(result);
        }

        [HttpGet("get")]
        public async Task<ActionResult<PaginatedList<ListingProductQueryDto>>> Listing([FromQuery] ListingPurchaseOrdersQuery query)
        {
            var result = await Sender.Send(query);
            return HandleResponse(result);
        }
    }
    }





 
 