using ErrorOr;
using Microsoft.AspNetCore.Mvc;

namespace Api.Common.Errors
{
    public static class ErrorExtensions
    {
        public static IActionResult Problem(List<Error> errors)
        {
            if (errors.Count == 0)
            {
                return new StatusCodeResult(500);
            }

            return errors[0].Type switch
            {
                ErrorType.Validation => new BadRequestObjectResult(errors),
                ErrorType.NotFound => new NotFoundObjectResult(errors),
                ErrorType.Conflict => new ConflictObjectResult(errors),
                ErrorType.Unauthorized => new UnauthorizedResult(),
                _ => new ObjectResult(errors) { StatusCode = 500 }
            };
        }
    }
}
