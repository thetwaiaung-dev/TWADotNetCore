using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using TWADotNetCore.MVC.AppDbContext;
using TWADotNetCore.MVC.Dtos;
using TWADotNetCore.MVC.Helpers;
using TWADotNetCore.MVC.Models;

namespace TWADotNetCore.MVC.Controllers
{
    public class BlogController : Controller
    {
        private readonly BlogDbContext _context;
        private readonly LogHelper _logHelper;

        public BlogController(BlogDbContext context, LogHelper logHelper)
        {
            _context = context;
            _logHelper = logHelper;
        }

        public async Task<IActionResult> Index(int pageNo = 1, int pageSize = 10)
        {
            List<BlogModel> lst = await _context.Blogs
                                            .AsNoTracking()
                                            .OrderByDescending(x => x.Blog_Id)
                                            .Skip((pageNo - 1) * pageSize)
                                            .Take(pageSize)
                                            .ToListAsync();

            int count = await _context.Blogs.CountAsync();
            int pageCount = count / pageSize;

            if (count % pageSize > 0) pageCount++;

            PageSettingModel pageSetting = new PageSettingModel(pageNo, pageSize, pageCount, null);

            BlogListResponseModel model = new BlogListResponseModel()
            {
                BlogList = lst,
                PageSetting = pageSetting
            };

            return View(model);
        }

        public IActionResult CreateBlog()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SaveBlog(BlogDto dto)
        {
            if (!ModelState.IsValid)
            {
                return View("CreateBlog", dto);
            }

            BlogModel blog = ChangeModel.Change(dto);

            try
            {
                await _context.Blogs.AddAsync(blog);
            }
            catch(Exception e)
            {
                _logHelper.Error("Something was wrong in saving Blog => "+e.Message);
            }
            var result = await _context.SaveChangesAsync();

            TempData["IsSuccess"] = result > 0;
            TempData["Message"] = result > 0 ? "Create Successful" : "Create Failed.";
            return Redirect("/Blog");
        }

        public async Task<IActionResult> EditBlog(int id)
        {
            bool isExist = await _context.Blogs.AsNoTracking().AnyAsync(x => x.Blog_Id == id);

            if (!isExist)
            {
                TempData["IsSuccess"] = false;
                TempData["Message"] = "No data found.";
                return Redirect("/Blog");
            }

            var item = await _context.Blogs.FirstOrDefaultAsync(x => x.Blog_Id == id);
            if (item == null)
            {
                TempData["IsSuccess"] = false;
                TempData["Message"] = "No data found.";
                return Redirect("/Blog");
            }

            BlogDto model = ChangeModel.Change(item);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateBlog(int id, BlogDto dto)
        {
            bool isExist = await _context.Blogs.AsNoTracking().AnyAsync(x => x.Blog_Id == id);
            if (!isExist)
            {
                TempData["IsSuccess"] = false;
                TempData["Message"] = "No data found.";
                return Redirect("/Blog");
            }

            var item = await _context.Blogs.FirstOrDefaultAsync(x => x.Blog_Id == id);
            if (item == null)
            {
                TempData["IsSuccess"] = false;
                TempData["Message"] = "No data found.";
                return Redirect("/Blog");
            }

            BlogModel blog = ChangeModel.Change(dto);
            item.Blog_Title = blog.Blog_Title;
            item.Blog_Author = blog.Blog_Author;
            item.Blog_Content = blog.Blog_Content;

            var result = await _context.SaveChangesAsync();
            TempData["IsSuccess"] = result > 0;
            TempData["Message"] = result > 0 ? "Update Successful." : "Update Failed.";

            return Redirect("/Blog");
        }

        public async Task<IActionResult> DeleteBlog(int id)
        {
            bool isExist = await _context.Blogs.AsNoTracking().AnyAsync(x => x.Blog_Id == id);

            if (!isExist)
            {
                TempData["IsSuccess"] = false;
                TempData["Message"] = "No data found.";
                return Redirect("/Blog");
            }

            var item = await _context.Blogs.FirstOrDefaultAsync(x => x.Blog_Id == id);
            if (item == null)
            {
                TempData["IsSuccess"] = false;
                TempData["Message"] = "No data found.";
                return Redirect("/Blog");
            }

            _context.Blogs.Remove(item);
            var result = await _context.SaveChangesAsync();

            TempData["IsSuccess"] = result > 0;
            TempData["Message"] = result > 0 ? "Delete Successful." : "Delete Failed.";
            return Redirect("/Blog");
        }
    }
}
