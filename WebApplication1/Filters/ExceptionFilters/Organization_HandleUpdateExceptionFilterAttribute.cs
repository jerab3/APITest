using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using WebApplication1.Models.Repositories;

namespace WebApplication1.Filters.ExceptionFilters
{
    public class Organization_HandleUpdateExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            base.OnException(context);

            var strId = context.RouteData.Values["id"] as string;
            if (int.TryParse(strId, out var id))
            {
                if (!OrganizationRepository.OrganizationExists(id))
                {
                    context.ModelState.AddModelError("Organization", "Organization doesn't exist anymore.");
                    var problemDetails = new ValidationProblemDetails(context.ModelState)
                    {
                        Status = StatusCodes.Status404NotFound
                    };
                    context.Result = new NotFoundObjectResult(problemDetails);
                }
            }
        }
    }
}
