using Microsoft.AspNetCore.Mvc;

namespace NetCoreASPMVC.Controllers {
    public class ProductsController : Controller {
        public IActionResult Index() {
            return View();
        }
    }
}
