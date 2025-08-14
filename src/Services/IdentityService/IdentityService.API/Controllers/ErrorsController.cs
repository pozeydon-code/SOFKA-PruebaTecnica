using Microsoft.AspNetCore.Diagnostics;

namespace IdentityService.API.Controllers;
[Route("api/v1/[controller]")]
public class ErrorController : ControllerBase
{
    [ApiExplorerSettings(IgnoreApi = true)]
    [Route("/error")]
    public IActionResult Error()
    {
        Exception? exception = HttpContext.Features.Get<IExceptionHandlerFeature>()?.Error;
        return Problem();
    }
}
