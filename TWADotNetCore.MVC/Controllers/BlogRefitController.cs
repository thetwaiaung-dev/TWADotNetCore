using Microsoft.AspNetCore.Mvc;
using Refit;
using System.Threading.Tasks;
using TWADotNetCore.MVC.Dtos;
using TWADotNetCore.MVC.Interfaces;
using TWADotNetCore.MVC.Models;

namespace TWADotNetCore.MVC.Controllers
{
    public class BlogRefitController : Controller
    {
        private readonly IBlogApi _blogApi;

        public BlogRefitController(IBlogApi blogApi)
        {
            _blogApi = blogApi;
        }

        public async Task<IActionResult> Index()
        {
            var model = await _blogApi.GetBlogs();
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
                model = await _blogApi.CreateBlog(blog);

                TempData["Message"] = model.Message;
                TempData["IsSuccess"] = model.IsSuccess;
                return RedirectToAction("Index", "BlogRefit");
            }

            TempData["Message"] = model.Message;
            TempData["IsSuccess"] = model.IsSuccess;
            return View("~/Views/BlogHttpClient/CreateBlog.cshtml", dto);
        }

        public async Task<IActionResult> EditBlog(int id)
        {
            var model = await _blogApi.GetBlog(id);

            TempData["Message"] = model.Message;
            TempData["IsSuccess"] = model.IsSuccess;
            if (model.IsSuccess)
            {
                BlogDto dto = model.Data.Change();
                return View("~/Views/BlogHttpClient/EditBlog.cshtml", dto);
            }

            return RedirectToAction("Index", "BlogRefit");
        }

        public async Task<IActionResult> UpdateBlog(int id, BlogDto dto)
        {
            BlogApiResponseModel model = new BlogApiResponseModel();
            if (ModelState.IsValid)
            {
                BlogModel blog = dto.Change();
                model = await _blogApi.UpdateBlog(id, blog);

                TempData["Message"] = model.Message;
                TempData["IsSuccess"] = model.IsSuccess;
                if (model.IsSuccess)
                {
                    return RedirectToAction("Index", "BlogRefit");
                }
            }

            return View("~/Views/BlogHttpClient/EditBlog.cshtml", dto);
        }

        public async Task<IActionResult> DeleteBlog(int id)
        {
            var model = await _blogApi.DeleteBlog(id);

            TempData["Message"] = model.Message;
            TempData["IsSuccess"] = model.IsSuccess;
            return RedirectToAction("Index", "BlogRefit");
        }
    }
}
