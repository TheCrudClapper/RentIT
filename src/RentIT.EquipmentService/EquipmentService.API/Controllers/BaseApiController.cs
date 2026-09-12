using EquipmentService.API.Extensions;
using EquipmentService.Core.Domain.ResultTypes;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace EquipmentService.API.Controllers
{
    public class BaseApiController : ControllerBase
    {
        protected Guid CurrentUserId => this.GetLoggedUserId();

        public ActionResult HandleResult<T>(Result<T> result)
        {
            return result.IsFailure
                ? Problem(title: result.Error.Code, detail: result.Error.Description, statusCode: (int)MapToStatusCode(result.Error))
                : Ok(result.Value);
        }

        protected IActionResult HandleResult(Result result)
        {
            return result.IsFailure
                ? Problem(title: result.Error.Code, detail: result.Error.Description, statusCode: (int)MapToStatusCode(result.Error))
                : NoContent();
        }

        private static HttpStatusCode MapToStatusCode(Error error)
          => error.Type switch
          {
              ErrorType.Validation => HttpStatusCode.BadRequest,
              ErrorType.NotFound => HttpStatusCode.NotFound,
              ErrorType.Conflict => HttpStatusCode.Conflict,
              ErrorType.Unauthorized => HttpStatusCode.Unauthorized,
              ErrorType.Forbidden => HttpStatusCode.Forbidden,
              _ => HttpStatusCode.InternalServerError,
          };
    }
}
