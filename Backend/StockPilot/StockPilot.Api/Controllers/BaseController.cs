using MediatR;
using Microsoft.AspNetCore.Mvc;
using StockPilot.Domain.Common;

namespace StockPilot.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public abstract class BaseController : ControllerBase
    {
        protected ISender Sender { get; }

        protected BaseController(ISender sender)
        {
            Sender = sender;
        }

  
        protected ActionResult HandleResponse(Result result)
        {
            if (result.IsSuccess)
            {
                return result.StatusCode switch
                {
                    201 => Created(),
                    202 => Accepted(),
                    204 => NoContent(),
                    _ => Ok(new { IsSuccess = true, StatusCode = result.StatusCode })
                };
            }

            return MatchFailureResponse(result);
        }

        protected ActionResult HandleResponse<T>(Result<T> result)
        {
            if (result.IsSuccess)
            {
                return result.StatusCode switch
                {
                    201 => Created(string.Empty, result.Value),
                    202 => Accepted(result.Value),
                    _ => Ok(result.Value) 
                };
            }

            return MatchFailureResponse(result);
        }

        private ActionResult MatchFailureResponse(Result result)
        {
            return result.StatusCode switch
            {
                400 => result.ValidationError != null && result.ValidationError.Count > 0
                    ? BadRequest(new { IsSuccess = false, Errors = result.ValidationError })
                    : BadRequest(new { IsSuccess = false, Error = result.ErrorMessage }),
                401 => Unauthorized(new { IsSuccess = false, Error = result.ErrorMessage ?? "Unauthorized" }),
                403 => Forbid(),
                404 => NotFound(new { IsSuccess = false, Error = result.ErrorMessage ?? "Not Found" }),
                409 => Conflict(new { IsSuccess = false, Error = result.ErrorMessage ?? "Conflict occurred" }),
                422 => UnprocessableEntity(new { IsSuccess = false, Error = result.ErrorMessage }),
                500 => StatusCode(500, new { IsSuccess = false, Error = result.ErrorMessage ?? "Internal Server Error" }),
                _ => BadRequest(new { IsSuccess = false, Error = "An unexpected error occurred." })
            };
        }
    }
}
