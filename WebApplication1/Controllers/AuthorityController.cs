using Microsoft.AspNetCore.Mvc;
using WebApplication1.Authority;

namespace WebApplication1.Controllers
{
    [ApiController]
    public class AuthorityController(IConfiguration configuration) : ControllerBase
    {
        private readonly IConfiguration _configuration = configuration;

        [HttpPost("auth")]
        public IActionResult Authenticate([FromBody] AppCredential credential)
        {
            if (Authenticator.Authenticate(credential.ClientId, credential.Secret))
            {
                var expiresAt = DateTime.UtcNow.AddMinutes(5);

                return Ok(new
                {
                    access_token = Authenticator.CreateToken(credential.ClientId, expiresAt, _configuration.GetValue<string>("SecretKey")),
                    expires_at = expiresAt
                });
            }

            ModelState.AddModelError("Unauthorized", "You are not authorized.");
            var problemDetails = new ValidationProblemDetails(ModelState)
            {
                Status = StatusCodes.Status401Unauthorized
            };
            return new UnauthorizedObjectResult(problemDetails);

        }
    }
}
