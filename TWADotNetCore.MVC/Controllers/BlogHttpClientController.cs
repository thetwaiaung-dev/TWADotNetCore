using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;
using TWADotNetCore.MVC.Models;

namespace TWADotNetCore.MVC.Controllers
{
    public class BlogHttpClientController : Controller
    {
        private readonly HttpClient _httpClient;

        public BlogHttpClientController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IActionResult> Index()
        {
            BlogListApiResponseModel model = new BlogListApiResponseModel();
            var response = await _httpClient.GetAsync("api/Blog");

            if (response.IsSuccessStatusCode)
            {
                string jsonStr = await response.Content.ReadAsStringAsync();
                model = JsonConvert.DeserializeObject<BlogListApiResponseModel>(jsonStr);
            }

            return View(model);
        }
    }
}
