using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using WebApplication1.Data;

namespace WebApplication1.Filters.ActionFilters;

public class Organization_ValidateOrganizationIdFilterAttribute(ApplicationDbContext db) : ActionFilterAttribute
{
    private readonly ApplicationDbContext _db = db;

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
            return;
        }

        var organization = _db.Organizations.Find(organizationId.Value);
        if (organization == null)
        {
            context.ModelState.AddModelError("Id", "Organization doesn't exist.");
            var problemDetails = new ValidationProblemDetails(context.ModelState)
            {
                Status = StatusCodes.Status404NotFound
            };
            context.Result = new NotFoundObjectResult(problemDetails);
            return;
        }
        else
        {
            context.HttpContext.Items["organization"] = organization;      
        }
    }
}
