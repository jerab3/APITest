using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using WebApplication1.Models.Repositories;

namespace WebApplication1.Filters.ActionFilters;

public class Organization_ValidateOrganizationIdFilterAttribute : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        base.OnActionExecuting(context);

        var organizationId = context.ActionArguments["id"] as int?;
        if (!organizationId.HasValue)
            return;

        if (organizationId.Value < 0)
        {
            context.ModelState.AddModelError("Id", "Id of organizaion is invalid.");
            var problemDetails = new ValidationProblemDetails(context.ModelState)
            {
                Status = StatusCodes.Status400BadRequest
            };
            context.Result = new BadRequestObjectResult(problemDetails);
        }
        else if (!OrganizationRepository.OrganizationExists(organizationId.Value))
        {
            context.ModelState.AddModelError("Id", "Organization doesn't exist.");
            var problemDetails = new ValidationProblemDetails(context.ModelState)
            {
                Status = StatusCodes.Status404NotFound
            };
            context.Result = new NotFoundObjectResult(problemDetails);
        }
    }
}
