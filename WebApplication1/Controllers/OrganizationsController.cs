using Microsoft.AspNetCore.Mvc;
using WebApplication1.Filters;
using WebApplication1.Models;
using WebApplication1.Models.Repositories;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrganizationsController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetOrganizations()
        {
            return Ok(OrganizationRepository.GetOrganizations());
        }

        [HttpGet("{id}")]
        [Organization_ValidateOrganizationIdFilter]
        public IActionResult GetOrganizationById(int id)
        {
            return Ok(OrganizationRepository.GetOrganizationById(id));
        }

        [HttpPost]
        [Organization_ValidateCreateOrganizationFilter]
        public IActionResult CreateOrganization([FromBody] Organization organization)
        {
            OrganizationRepository.AddOrganization(organization);
            
            return CreatedAtAction(nameof(GetOrganizationById),
                new{ id = organization.Id },
                organization);
        }

        [HttpDelete("{id}")]
        public string DeleteOrganization(int id)
        {
            return $"Deleting organization with ID: {id}";
        }

        [HttpPut("{id}")]
        [Organization_ValidateOrganizationIdFilter]
        [Organization_ValidateUpdateOrganizationFilter]
        public IActionResult UpdateOrganization(int id, [FromBody] Organization organization)
        {
            try
            {
                OrganizationRepository.UpdateOrganization(organization);
            }
            catch
            {
                if (!OrganizationRepository.OrganizationExists(id))
                {
                    return NotFound();
                }

                throw;
            }

            return NoContent();
        }
    }
}
