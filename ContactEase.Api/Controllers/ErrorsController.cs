using Microsoft.AspNetCore.Mvc;

namespace ContactEase.Api.Controllers;

public class ErrorsController : ControllerBase
{
    [Route("/error")]
    public IActionResult Error()
    {
        return Problem();
    }
}
