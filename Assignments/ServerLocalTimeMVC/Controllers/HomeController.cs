using Microsoft.AspNetCore.Mvc;
using ServerLocalTimeMVC.Models;
using System.Diagnostics;

namespace ServerLocalTimeMVC.Controllers {
  public class HomeController : Controller {
    private readonly ILogger<HomeController> _logger;
    static ulong HitCount = 0;

    public HomeController(ILogger<HomeController> logger) => _logger = logger;

    public IActionResult Index() {
      ViewData["HitCount"] = ++HitCount;
      return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error() 
      => View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
  }
}
