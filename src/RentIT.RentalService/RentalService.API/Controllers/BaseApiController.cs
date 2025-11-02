using Microsoft.AspNetCore.Mvc;
using RentalService.API.Extensions;
using RentalService.Core.ResultTypes;

namespace RentalService.API.Controllers
{
    public class BaseApiController : ControllerBase
    {
        protected Guid CurrentUserId => this.GetLoggedUserId();

        public ActionResult HandleResult<T>(Result<T> result)
        {
            return result.IsFailure
                ? Problem(detail: result.Error.Description, statusCode: result.Error.StatusCode)
                : Ok(result.Value);
        }

        protected IActionResult HandleResult(Result result)
        {
            return result.IsFailure
                ? Problem(detail: result.Error.Description, statusCode: result.Error.StatusCode)
                : NoContent();
        }
    }
}
