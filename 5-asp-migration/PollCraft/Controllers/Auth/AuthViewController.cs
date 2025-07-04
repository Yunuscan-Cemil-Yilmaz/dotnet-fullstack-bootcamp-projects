using Microsoft.AspNetCore.Mvc;

namespace PollCraft.Controllers.Auth;

[Route("AuthView")]
public class AuthViewController : Controller
{
    [Route("Index")]
    public IActionResult Index() // endpoint is /AuthView/Index
    {
        return View("~/Views/Auth/Index.cshtml");
    }
}