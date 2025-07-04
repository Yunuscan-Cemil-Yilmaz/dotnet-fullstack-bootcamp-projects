using Microsoft.AspNetCore.Mvc;

namespace PollCraft.Controllers;

public class ErrorsController : Controller
{
    public IActionResult Unauthorized()
    {
        return View("Unauthorized");
    }

    public IActionResult SystemError()
    {
        return View("SystemError");
    }

    public IActionResult NotFound()
    {
        return View("NotFound");
    }
}
