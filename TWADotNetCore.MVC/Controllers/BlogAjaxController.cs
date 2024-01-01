using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using TWADotNetCore.MVC.AppDbContext;
using TWADotNetCore.MVC.Dtos;
using TWADotNetCore.MVC.Models;

namespace TWADotNetCore.MVC.Controllers
{
    public class BlogAjaxController : Controller
    {
        private readonly BlogDbContext _context;

        public BlogAjaxController(BlogDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [ActionName("list")]
        public IActionResult BlogList(int pageNo = 1, int pageSize = 10)
        {
            var blogList = _context.Blogs
                                    .AsNoTracking()
                                    .OrderByDescending(x => x.Blog_Id)
                                    .Skip((pageNo - 1) * pageSize)
                                    .Take(pageSize)
                                    .ToList();

            int count = _context.Blogs.Count();
            int pageCount = count / pageSize;
            if (count % pageSize > 0) pageCount++;

            PageSettingModel pageSetting = new PageSettingModel(pageNo, pageSize, pageCount, "/blogajax/list");

            BlogListResponseModel model = new BlogListResponseModel()
            {
                BlogList = blogList,
                PageSetting = pageSetting

            };

            return View("BlogList", model);
        }

        [ActionName("create")]
        public IActionResult CreateBlog()
        {
            return View("CreateBlog");
        }

        [HttpPost]
        public IActionResult SaveBlog(BlogDto reqModel)
        {
            MessageModel message = new MessageModel();
            if (!ModelState.IsValid)
            {
                message.IsSuccess = false;
                message.Message = "Field is required.";

                return Json(message);
            }

            BlogModel blog = reqModel.Change();
            _context.Blogs.Add(blog);
            var result = _context.SaveChanges();

            message.IsSuccess = result > 0;
            message.Message = result > 0 ? "Create Success" : "Create failed.";

            TempData["Message"] = message.Message;
            TempData["IsSuccess"] = message.IsSuccess;
            return Json(message);
        }

        [ActionName("edit")]
        public IActionResult EditBlog(int id)
        {
            BlogModel blog = _context.Blogs.AsNoTracking().FirstOrDefault(x => x.Blog_Id == id);
            if (blog is null)
            {
                TempData["Message"] = "No data found.";
                TempData["IsSuccess"] = false;
                return Redirect("/blogajax/list");
            }

            BlogDto model = blog.Change();
            return View("EditBlog", model);
        }

        [HttpPost]
        public IActionResult UpdateBlog(BlogDto reqModel)
        {
            BlogModel blog = _context.Blogs.FirstOrDefault(x => x.Blog_Id == reqModel.Blog_Id);

            MessageModel model = new MessageModel();
            if (blog is null)
            {
                model.Message = "No Data Found";
                model.IsSuccess = false;

                TempData["Message"] = model.Message;
                TempData["IsSuccess"] = model.IsSuccess;
                return Json(model);
            }

            blog.Blog_Title = reqModel.Blog_Title;
            blog.Blog_Author = reqModel.Blog_Author;
            blog.Blog_Content = reqModel.Blog_Content;

            var result = _context.SaveChanges();
            model.Message = result > 0 ? "Saving successful" : "Saving failed.";
            model.IsSuccess = result > 0;

            return Json(model);
        }

        public IActionResult DeleteBlog(int id)
        {
            MessageModel model = new MessageModel();

            if (!_context.Blogs.AsNoTracking().Any(x => x.Blog_Id == id))
            {
                model.Message = "No Data Found";
                model.IsSuccess = false;

                TempData["Message"] = model.Message;
                TempData["IsSuccess"] = model.IsSuccess;
                return Json(model);
            }

            BlogModel blog = _context.Blogs.FirstOrDefault(x => x.Blog_Id == id);

            _context.Blogs.Remove(blog);
            var result = _context.SaveChanges();

            model.Message = result > 0 ? "Saving successful" : "Saving failed.";
            model.IsSuccess = result > 0;
            return Json(model);
        }
    }
}
