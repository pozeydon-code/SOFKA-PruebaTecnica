using Microsoft.AspNetCore.Diagnostics;

namespace AccountService.API.Controllers;
[Route("[controller]")]
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
