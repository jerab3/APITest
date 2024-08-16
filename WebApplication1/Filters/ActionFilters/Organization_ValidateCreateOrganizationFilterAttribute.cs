using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using WebApplication1.Data;
using WebApplication1.Models;
using WebApplication1.Models.Repositories;

namespace WebApplication1.Filters.ActionFilters;

public class Organization_ValidateCreateOrganizationFilterAttribute(ApplicationDbContext db) : ActionFilterAttribute
{
    private readonly ApplicationDbContext _db = db;

    public override void OnActionExecuting(ActionExecutingContext context)
    {
        base.OnActionExecuting(context);

        var organization = context.ActionArguments["organization"] as Organization;

        if (organization == null)
        {
            context.ModelState.AddModelError("Organization", "Organization object is null.");
            var problemDetails = new ValidationProblemDetails(context.ModelState)
            {
                Status = StatusCodes.Status400BadRequest
            };
            context.Result = new BadRequestObjectResult(problemDetails);

            return;
        }

        var existingOrganization = _db.Organizations.FirstOrDefault(x =>
            x.Name.ToLower() == organization.Name.ToLower() 
            && !string.IsNullOrWhiteSpace(organization.Name));
        
        if (existingOrganization != null)
        {
            context.ModelState.AddModelError("Organization", "Organization with this name already exists.");
            var problemDetails = new ValidationProblemDetails(context.ModelState)
            {
                Status = StatusCodes.Status400BadRequest
            };
            context.Result = new BadRequestObjectResult(problemDetails);

            return;
        }
        
    }
}
