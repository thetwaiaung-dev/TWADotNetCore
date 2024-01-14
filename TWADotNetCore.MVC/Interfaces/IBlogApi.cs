using Refit;
using System.Threading.Tasks;
using TWADotNetCore.MVC.Models;

namespace TWADotNetCore.MVC.Interfaces
{
    public interface IBlogApi
    {
        [Get("/api/blog")]
        Task<BlogListApiResponseModel> GetBlogs();

        [Get("/api/blog/{pageNo}/{pageSize}")]
        Task<BlogListApiResponseModel> GetBlogs(int pageNo, int pageSize);

        [Get("/api/blog/{id}")]
        Task<BlogApiResponseModel> GetBlog(int id);

        [Post("/api/blog")]
        Task<BlogApiResponseModel> CreateBlog(BlogModel blog);

        [Put("/api/blog/{id}")]
        Task<BlogApiResponseModel> UpdateBlog(int id, BlogModel blog);

        [Delete("/api/blog/{id}")]
        Task<BlogApiResponseModel> DeleteBlog(int id);
    }
}
