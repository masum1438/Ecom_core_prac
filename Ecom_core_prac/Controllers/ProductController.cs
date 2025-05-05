using Microsoft.AspNetCore.Mvc;

namespace Ecom_core_prac.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult SingleProduct()
        {
            //@Context.Request.Query["id"].ToString()
            int id = Convert.ToInt32(HttpContext.Request.Query["id"].ToString());
            return View();
        }
        public IActionResult CartDetails()
        {
            return View();
        }
        public IActionResult Checkout()
        {
            return View();
        }
    }
}
