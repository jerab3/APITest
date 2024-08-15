using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using WebApplication1.Models;

namespace WebApplication1.Filters;

public class Organization_ValidateUpdateOrganizationFilterAttribute : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        base.OnActionExecuting(context);

        var id = context.ActionArguments["id"] as int?;
        var organization = context.ActionArguments["organization"] as Organization;

        if (id.HasValue && organization != null && id != organization.Id)
        {
            context.ModelState.AddModelError("Id", "Id of organization is not the same as id.");
            var problemDetails = new ValidationProblemDetails(context.ModelState)
            {
                Status = StatusCodes.Status400BadRequest
            };
            context.Result = new BadRequestObjectResult(problemDetails);
        }
    }
}
