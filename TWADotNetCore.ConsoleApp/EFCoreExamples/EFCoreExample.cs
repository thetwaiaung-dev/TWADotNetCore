using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using TWADotNetCore.ConsoleApp.Models;

namespace TWADotNetCore.ConsoleApp.EFCoreExamples
{
    public class EFCoreExample
    {
        public void Run()
        {
            //Create("EFCore Title", "EFCore Author", "EFCore content");
            //Edit(5);
            //Update(5,"EFCore Title update","EFCore Author update","EFCore content update");
            Delete(2);
            Read();
        }

        public void Read()
        {
            AppDbContext dbContext = new AppDbContext();
            var lst = dbContext.BlogModels.OrderByDescending(b => b.Blog_Id).ToList();
            foreach ( var item in lst )
            {
                Console.WriteLine(item.Blog_Id);
                Console.WriteLine(item.Blog_Title);
                Console.WriteLine(item.Blog_Author);
                Console.WriteLine(item.Blog_Content);
            }
        }

        public void Create(string title,string author,string content)
        {
            BlogModel blog = new BlogModel()
            {
                Blog_Title = title,
                Blog_Author = author,
                Blog_Content = content
            };
            AppDbContext db = new AppDbContext();
            db.Add(blog);
            var result=db.SaveChanges();

            var message = result > 0 ? "Saving successful." : "Saving failed.";
            Console.WriteLine(message); 
        }

        public void Edit(int id)
        {
            AppDbContext db = new AppDbContext();
            var blog=db.BlogModels
                        .Where(b=>b.Blog_Id == id)
                        .FirstOrDefault();
            if (blog == null)
            {
                Console.WriteLine("No data found.");
                return ;
            }
            Console.WriteLine(blog.Blog_Id);
            Console.WriteLine(blog.Blog_Title);
            Console.WriteLine(blog.Blog_Author);
            Console.WriteLine(blog.Blog_Content);
        }

        public void Update(int id,string title,string author,string content)
        {
            AppDbContext db = new AppDbContext();
            BlogModel updateBlog=db.BlogModels.Where(b=>b.Blog_Id==id).FirstOrDefault();

            if (updateBlog == null)
            {
                Console.WriteLine("No data found.");
                return;
            }

            updateBlog.Blog_Title = title;
            updateBlog.Blog_Author = author;
            updateBlog.Blog_Content = content;
            var result = db.SaveChanges();

            var message = result > 0 ? "Update successful." : "Update failed.";
            Console.WriteLine(message);
        }

        public void Delete(int id)
        {
            AppDbContext db = new AppDbContext();
            BlogModel blog = db.BlogModels.FirstOrDefault(b => b.Blog_Id == id);

            if(blog == null)
            {
                Console.WriteLine("No data found.");
                return;
            }

            db.BlogModels.Remove(blog);
            var result = db.SaveChanges();
            var message = result > 0 ? "Delete successful." : "Delete failed.";

            Console.WriteLine(message);
        }



    }
}
