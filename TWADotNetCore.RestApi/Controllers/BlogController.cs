using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using TWADotNetCore.RestApi.Dtos;
using TWADotNetCore.RestApi.EFCoreExamples;
using TWADotNetCore.RestApi.Models;
using TWADotNetCore.RestApi.LimitRequests;
using Microsoft.AspNetCore.RateLimiting;

namespace TWADotNetCore.RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[EnableRateLimiting("fixed")]
    public class BlogController : ControllerBase
    {
        private readonly AppDbContext _db;

        public BlogController(AppDbContext dbContext)
        {
            _db = dbContext;
        }

        [HttpGet]
        //[EnableRateLimiting("fixed")]
        public IActionResult GetAllBlogs()
        {
            var lst = _db.Blogs.OrderByDescending(b => b.Blog_Id).ToList();

            BlogListResponseModel data = new BlogListResponseModel()
            {
                IsSuccess = true,
                Message = "Success",
                Data = lst
            };

            return Ok(data);
        }

        [HttpGet("{pageNo}/{pageSize}")]
        public IActionResult GetAllBlogs(int pageNo, int pageSize)
        {

            var lst = _db.Blogs.OrderByDescending(x => x.Blog_Id)
                         .Skip((pageNo - 1) * pageSize)
                         .Take(pageSize)
                         .ToList();

            BlogListResponseModel model = new BlogListResponseModel()
            {
                IsSuccess = true,
                Message = "Success",
                Data = lst
            };
            return Ok(model);
        }

        [HttpGet("{id}")]
        public IActionResult GetBlog(int id)
        {
            var blog = _db.Blogs.Where(b => b.Blog_Id == id).FirstOrDefault();

            BlogResponseModel data = new BlogResponseModel();
            if (blog is null)
            {   
                data.IsSuccess = false;
                data.Message = "No data found";
                return NotFound(data);
            }

            data.IsSuccess = true;
            data.Message = "Success";
            data.Data = blog;
            return Ok(data);
        }

        [HttpPost]  
        public IActionResult Create([FromBody] BlogModel blog)
        {
            _db.Blogs.Add(blog);
            var result = _db.SaveChanges();

            string message = result > 0 ? "Success" : "Failed.";

            BlogResponseModel data = new BlogResponseModel()
            {
                IsSuccess = result > 0,
                Message = message,
            };
            return Ok(data);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] BlogModel blog)
        {
            var updateBlog = _db.Blogs.Where(b => b.Blog_Id == id).FirstOrDefault();

            BlogResponseModel data = new BlogResponseModel();
            if (updateBlog is null)
            {
                data.IsSuccess = false;
                data.Message = "No data found";
                return NotFound(data);
            }

            updateBlog.Blog_Title = blog.Blog_Title;
            updateBlog.Blog_Author = blog.Blog_Author;
            updateBlog.Blog_Content = blog.Blog_Content;
            _db.SaveChanges();

            data.IsSuccess = true;
            data.Message = "Success";
            data.Data = updateBlog;
            return Ok(data);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var blog = _db.Blogs.Where(b => b.Blog_Id == id).FirstOrDefault();

            BlogResponseModel data = new BlogResponseModel();
            if (blog is null)
            {
                data.IsSuccess = false;
                data.Message = "No data found.";
                return NotFound(data);
            }

            _db.Blogs.Remove(blog);
            _db.SaveChanges();
            data.IsSuccess = true;
            data.Message = "Success.";
            return Ok(data);
        }

        [HttpPatch("{id}")]
        public IActionResult PatchBlog(int id, [FromBody] JsonPatchDocument<BlogModel> blogPatch)
        {
            var blog = _db.Blogs.FirstOrDefault(b => b.Blog_Id == id);

            BlogResponseModel data = new BlogResponseModel();
            if (blog is null)
            {
                data.IsSuccess = false;
                data.Message = "No data found";
                return NotFound(data);
            }

            blogPatch.ApplyTo(blog);
            _db.SaveChanges();
            data.IsSuccess = true;
            data.Message = "Success";
            data.Data = blog;

            return Ok(data);
        }
    }
}
