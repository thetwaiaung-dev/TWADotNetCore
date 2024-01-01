using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using TWADotNetCore.MVC.AppDbContext;
using TWADotNetCore.MVC.Models;

namespace TWADotNetCore.MVC.Services
{
    public class BlogService
    {
        private readonly BlogDbContext _context;

        public BlogService(BlogDbContext context)
        {
            _context = context;
        }

        public List<BlogModel> GetAll()
        {
            return _context.Blogs.AsNoTracking().ToList();
        }

        public int Save(BlogModel blog)
        {
            _context.Blogs.Add(blog);
            return _context.SaveChanges();
        }
    }
}
