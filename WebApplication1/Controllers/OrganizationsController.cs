using Microsoft.AspNetCore.Mvc;
using WebApplication1.Data;
using WebApplication1.Filters.ActionFilters;
using WebApplication1.Filters.ExceptionFilters;
using WebApplication1.Models;

namespace WebApplication1.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrganizationsController(ApplicationDbContext db) : ControllerBase
{
    private readonly ApplicationDbContext _db = db;

    [HttpGet]
    public IActionResult GetOrganizations()
    {
        return Ok(_db.Organizations.ToList());
    }

    [HttpGet("{id}")]
    [TypeFilter(typeof(Organization_ValidateOrganizationIdFilterAttribute))]
    public IActionResult GetOrganizationById(int id)
    {
        return Ok(HttpContext.Items["organization"]);
    }

    [HttpPost]
    [TypeFilter(typeof(Organization_ValidateCreateOrganizationFilterAttribute))]
    public IActionResult CreateOrganization([FromBody] Organization organization)
    {
        _db.Organizations.Add(organization);
        _db.SaveChanges();
        
        return CreatedAtAction(nameof(GetOrganizationById),
            new{ id = organization.Id },
            organization);
    }

    [HttpDelete("{id}")]
    [TypeFilter(typeof(Organization_ValidateOrganizationIdFilterAttribute))]
    public IActionResult DeleteOrganization(int id)
    {
        var organizationToDelete = HttpContext.Items["organization"] as Organization;

        _db.Organizations.Remove(organizationToDelete);
        _db.SaveChanges();

        return Ok(organizationToDelete);
    }

    [HttpPut("{id}")]
    [TypeFilter(typeof(Organization_ValidateOrganizationIdFilterAttribute))]
    [Organization_ValidateUpdateOrganizationFilter]
    [TypeFilter(typeof(Organization_HandleUpdateExceptionFilterAttribute))]
    public IActionResult UpdateOrganization(int id, [FromBody] Organization organization)
    {
        var organizationToUpdate = HttpContext.Items["organization"] as Organization;

        organizationToUpdate.Name = organization.Name;
        organizationToUpdate.NumberOfEmployees = organization.NumberOfEmployees;

        _db.SaveChanges();

        return NoContent();
    }
}
