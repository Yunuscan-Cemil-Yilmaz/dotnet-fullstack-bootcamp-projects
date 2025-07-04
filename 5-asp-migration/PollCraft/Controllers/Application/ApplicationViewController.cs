using Microsoft.AspNetCore.Mvc;
using PollCraft.Infrastructure.Data;
using PollCraft.Models;
using System.Linq;

namespace PollCraft.Controllers.Application;

[Route("application")]
public class ApplicationViewController : Controller
{
    private readonly AppDbContext _context;
    public ApplicationViewController(AppDbContext context)
    {
        _context = context;
    }
    [Route("/application/News/{token}/{userId}")]
    public IActionResult News(string token, int userId)
    {
        var user = _context.Users.FirstOrDefault(u => u.Id == userId);
        if (user == null) return Redirect("/AuthView/Index");
        return View("~/Views/Application/News.cshtml", user);
    }
}