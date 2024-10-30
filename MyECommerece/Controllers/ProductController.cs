using Microsoft.AspNetCore.Mvc;

namespace MyECommerece.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
