using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Threading.Tasks;
using TWADotNetCore.MVC.Dtos;
using TWADotNetCore.MVC.Models;

namespace TWADotNetCore.MVC.Controllers
{
    public class BlogRestClientController : Controller
    {
        private readonly RestClient _restClient;

        public BlogRestClientController(RestClient restClient)
        {
            _restClient = restClient;
        }

        public async Task<IActionResult> Index()
        {
            BlogListApiResponseModel model = new BlogListApiResponseModel();
            RestRequest request = new RestRequest("api/Blog", Method.Get);

            //await _restClient.GetAsync(request);
            var response = await _restClient.ExecuteAsync(request);
            if (response.IsSuccessStatusCode)
            {
                string jsonStr = response.Content;
                model = JsonConvert.DeserializeObject<BlogListApiResponseModel>(jsonStr);
            }

            return View("~/Views/BlogHttpClient/Index.cshtml", model);
        }

        public IActionResult CreateBlog()
        {
            return View("~/Views/BlogHttpClient/CreateBlog.cshtml");
        }

        [HttpPost]
        public async Task<IActionResult> SaveBlog(BlogDto dto)
        {
            BlogApiResponseModel model = new BlogApiResponseModel();
            if (ModelState.IsValid)
            {
                BlogModel blog = dto.Change();
                RestRequest request = new RestRequest("api/Blog", Method.Post);
                request.AddJsonBody(blog);

                var response = await _restClient.ExecuteAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    string jsonStr = response.Content;
                    model = JsonConvert.DeserializeObject<BlogApiResponseModel>(jsonStr);

                    TempData["Message"] = model.Message;
                    TempData["IsSuccess"] = model.IsSuccess;
                    return RedirectToAction("Index", "BlogRestClient");
                }
            }

            TempData["Message"] = model.Message;
            TempData["IsSuccess"] = model.IsSuccess;
            return View("~/Views/BlogHttpClient/CreateBlog.cshtml", dto);
        }

        public async Task<IActionResult> EditBlog(int id)
        {
            BlogApiResponseModel model = new BlogApiResponseModel();
            RestRequest request = new RestRequest($"api/Blog/{id}");

            var response = await _restClient.ExecuteAsync(request);
            if (response.IsSuccessStatusCode)
            {
                string jsonStr = response.Content;
                model = JsonConvert.DeserializeObject<BlogApiResponseModel>(jsonStr);

                BlogDto dto = model.Data.Change();
                return View("~/Views/BlogHttpClient/EditBlog.cshtml", dto);
            }

            TempData["Message"] = model.Message;
            TempData["IsSuccess"] = model.IsSuccess;
            return RedirectToAction("Index", "BlogRestClient");
        }

        [HttpPost]
        public async Task<IActionResult> UpdateBlog(int id, BlogDto dto)
        {
            BlogApiResponseModel model = new BlogApiResponseModel();
            if (ModelState.IsValid)
            {
                BlogModel blog = dto.Change();
                RestRequest request = new RestRequest($"api/Blog/{id}", Method.Put);
                request.AddJsonBody(blog);

                var response = await _restClient.ExecuteAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    string jsonStr = response.Content;
                    model = JsonConvert.DeserializeObject<BlogApiResponseModel>(jsonStr);

                    TempData["Message"] = model.Message;
                    TempData["IsSuccess"] = model.IsSuccess;
                    return RedirectToAction("Index", "BlogRestClient");
                }
            }

            TempData["Message"] = model.Message;
            TempData["IsSuccess"] = model.IsSuccess;
            return View("~/Views/BlogHttpClient/EditBlog.cshtml", dto);
        }

        public async Task<IActionResult> DeleteBlog(int id)
        {
            BlogApiResponseModel model = new BlogApiResponseModel();
            RestRequest request = new RestRequest($"https://localhost:7001/api/Blog/{id}", Method.Delete);

            var response = await _restClient.ExecuteAsync(request);
            if (response.IsSuccessStatusCode)
            {
                string jsonStr = response.Content;
                model = JsonConvert.DeserializeObject<BlogApiResponseModel>(jsonStr);
            }

            TempData["Message"] = model.Message;
            TempData["IsSuccess"] = model.IsSuccess;
            return RedirectToAction("Index", "BlogRestClient");
        }
    }
}
