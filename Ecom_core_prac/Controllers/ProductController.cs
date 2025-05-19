using Ecom_core_prac.Models;
using Microsoft.AspNetCore.Mvc;

namespace Ecom_core_prac.Controllers
{
    public class ProductController : Controller
    {
        


        private readonly AppDbContext _context;


        public ProductController(AppDbContext context)
        {
            _context = context;
           
        }

        public IActionResult Dashboard()
        {
            var equipments = _context.baseEquipments.ToList();
            return View(equipments);
        }
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
