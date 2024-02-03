using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using TWADotNetCore.ShoppingCartMvcApp.Models;
using TWADotNetCore.ShoppingCartMvcApp.Models.ViewModels;

namespace TWADotNetCore.ShoppingCartMvcApp.Controllers
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
            List<ProductViewModel> products = new();
            using StreamReader reader = new("product.json");
            var productJson = reader.ReadToEnd();
            products = JsonConvert.DeserializeObject<List<ProductViewModel>>(productJson);

            return View();
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