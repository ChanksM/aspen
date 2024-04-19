using Microsoft.AspNetCore.Mvc;
using MiddlewareComponents.Models;
using System.Diagnostics;

namespace MiddlewareComponents.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            var message = HttpContext.Items["Message"] as string;

            ViewData["MiddlewareMessage"] = message;

            var products = new List<Product>
            {
                new Product { Id = 1, Name = "ნესვი", Price = 10.99m },
                new Product { Id = 2, Name = "უხა", Price = 20.99m },
                new Product { Id = 3, Name = "სოსისი", Price = 30.99m }
            };

            return View(products);
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
