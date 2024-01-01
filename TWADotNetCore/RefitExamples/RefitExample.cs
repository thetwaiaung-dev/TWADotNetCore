using Newtonsoft.Json;
using Refit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TWADotNetCore.ConsoleApp.Models;

namespace TWADotNetCore.ConsoleApp.RefitExamples
{
    public class RefitExample
    {
        private readonly IBlogApi blogApi;
        public RefitExample()
        {
            blogApi = RestService.For<IBlogApi>("https://localhost:7001");
        }
            
        public async Task Run()
        {
            //await Delete(6);

            //await Update(13, "Refit title update", "Refit author update", "Refit content update");

            //await Create("Refit title", "Refit Author", "Refit content");

            //await Edit(5);

           //Console.WriteLine("Blogs by pagination...");

            await Read(1, 5);

            //await Read();
        }

        public async Task Read()
        {
            BlogListResponseModel model = await blogApi.GetBlogs();

            Console.WriteLine(JsonConvert.SerializeObject(model, Formatting.Indented));
        }

        public async Task Read(int pageNo,int pageSize)
        {
            BlogListResponseModel model= await blogApi.GetBlogs(pageNo,pageSize);

            Console.WriteLine(JsonConvert.SerializeObject(model,Formatting.Indented)); 
        }

        public async Task Edit(int id)
        {
            BlogResponseModel model=await blogApi.GetBlog(id);

            Console.WriteLine(JsonConvert.SerializeObject(model, Formatting.Indented));
        }

        public async Task Create(string title,string author,string content)
        {
            BlogResponseModel model = await blogApi.CreateBlog(new BlogModel()
            {
                Blog_Title = title,
                Blog_Author = author,
                Blog_Content = content
            });

            Console.WriteLine(JsonConvert.SerializeObject(model, Formatting.Indented));
        }

        public async Task Update(int id,string title,string author,string content)
        {
            BlogResponseModel model = await blogApi.UpdateBlog(id, new BlogModel()
            {
                Blog_Title = title,
                Blog_Author = author,
                Blog_Content = content
            });

            Console.WriteLine(JsonConvert.SerializeObject(model, Formatting.Indented));
        }

        public async Task Delete(int id)
        {
            BlogResponseModel model = await blogApi.DeleteBlog(id);

            Console.WriteLine(JsonConvert.SerializeObject(model, Formatting.Indented));
        }
    }

    public interface IBlogApi
    {
        [Get("/api/blog")]
        Task<BlogListResponseModel> GetBlogs();

        [Get("/api/blog/{pageNo}/{pageSize}")]
        Task<BlogListResponseModel> GetBlogs(int pageNo, int pageSize);

        [Get("/api/blog/{id}")]
        Task<BlogResponseModel> GetBlog(int id);

        [Post("/api/blog")]
        Task<BlogResponseModel> CreateBlog(BlogModel blog);

        [Put("/api/blog/{id}")]
        Task<BlogResponseModel> UpdateBlog(int id, BlogModel blog);

        [Delete("/api/blog/{id}")]
        Task<BlogResponseModel> DeleteBlog(int id);

    }
}
