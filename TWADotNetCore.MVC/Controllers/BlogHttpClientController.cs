using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TWADotNetCore.MVC.Dtos;
using TWADotNetCore.MVC.Models;
using static System.Net.Mime.MediaTypeNames;

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

        public IActionResult CreateBlog()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SaveBlog(BlogDto dto)
        {
            BlogApiResponseModel model = new BlogApiResponseModel();
            if (ModelState.IsValid)
            {
                BlogModel blog = dto.Change();

                string blogJson = JsonConvert.SerializeObject(blog);
                HttpContent httpContent = new StringContent(blogJson, Encoding.UTF8, Application.Json);

                var response = await _httpClient.PostAsync("api/Blog/", httpContent);
                if (response.IsSuccessStatusCode)
                {
                    string jsonStr = await response.Content.ReadAsStringAsync();
                    model = JsonConvert.DeserializeObject<BlogApiResponseModel>(jsonStr);

                    TempData["Message"] = model.Message;
                    TempData["IsSuccess"] = model.IsSuccess;
                    return Redirect("Index");
                }
            }

            TempData["Message"] = model.Message;
            TempData["IsSuccess"] = model.IsSuccess;
            return View("CreateBlog", dto);
        }
    }
}
