using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StockPilot.Application.Common.Model;
using StockPilot.Application.Dtos;
using StockPilot.Application.Features.Command.Supplier.CreateSupplier;
using StockPilot.Application.Features.Command.Supplier.DeleteSupplier;
using StockPilot.Application.Features.Command.Supplier.UpdateSupplier;
using StockPilot.Application.Features.Queries.Supplier.GetSupplier;
using StockPilot.Application.Features.Queries.Supplier.GetSupplierById;
using StockPilot.Application.Features.Queries.Supplier.GetSupplierProducts;
using StockPilot.Application.Features.Queries.Supplier.GetSupplierPurchaseOrders;


namespace StockPilot.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SupplierController : BaseController

    {
        public SupplierController(ISender sender) : base(sender) { }


        [HttpPost("create")]
        public async Task<ActionResult> Create([FromBody] CreateSupplierCommand command)
        {
            var result = await Sender.Send(command);
            return HandleResponse(result);
        }

        [HttpPatch("update")]
        public async Task<ActionResult> Update([FromBody] UpdateSupplierCommand command)
        {
            var result = await Sender.Send(command);
            return HandleResponse(result);
        }

        [HttpDelete("delete/{id:guid}")]
        public async Task<ActionResult> Delete([FromRoute] Guid id)
        {
            var result = await Sender.Send(new DeleteSupplierCommand(id));
            return HandleResponse(result);
        }

        [HttpGet("list-suppliers")]
        public async Task<ActionResult<PaginatedList<SupplierListItemDto>>> GetSupplier([FromQuery] GetSupplierQuery query)
        {
            var result = await Sender.Send(query);
            return HandleResponse(result);
        }
        [HttpGet("get/{id}")]
        public async Task<ActionResult<GetSupplierDto>> GetSupplierById([FromRoute] Guid id)
        {
            var query = new GetSupplierByIdQuery(id);
            var result = await Sender.Send(query);
            return HandleResponse(result);
        }
        [HttpGet("get-suppliers-product")]
        public async Task<ActionResult<PaginatedList<ProductDto>>> GetSupplierProducts([FromQuery] GetSupplierProductsQuery query)
        {
            var result = await Sender.Send(query);
            return HandleResponse(result);
        }
        [HttpDelete("get-suppliers-purchase-order")]
        public async Task<ActionResult<List<SupplierPurchaseOrderDto>>> GetSupplierPurchaseOrders([FromQuery] GetSupplierPurchaseOrdersQuery query)
        {
            var result = await Sender.Send(query);
            return HandleResponse(result);
        }
    }
}

