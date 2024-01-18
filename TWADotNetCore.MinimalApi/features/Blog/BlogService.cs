using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TWADotNetCore.MinimalApi.Models;

namespace TWADotNetCore.MinimalApi.features.Blog
{
    public static class BlogService
    {
        public static void AddBlogService(this IEndpointRouteBuilder app)
        {
            #region GetBlogs
            app.MapGet("/blog/{pageNo}/{pageSize}", async ([FromServices] AppDbContext db, int pageNo, int pageSize) =>
            {
                var blogs = await db.Blogs
                                .AsNoTracking()
                                .Skip((pageNo - 1) * pageSize)
                                .Take(pageSize)
                                .ToListAsync();

                return Results.Ok(new BlogListResponseModel
                {
                    Message = blogs.Count > 0 ? "Successful" : "Failed",
                    IsSuccess = blogs.Count > 0,
                    Data = blogs
                });
            })
            .WithName("GetBlogs")
            .WithOpenApi();
            #endregion

            #region EditBlog

            app.MapGet("/blog/{id}", async ([FromServices] AppDbContext db, int id) =>
            {
                var blog = await db.Blogs.Where(x => x.Blog_Id == id).FirstOrDefaultAsync();

                return Results.Ok(new BlogResponseModel
                {
                    IsSuccess = blog != null,
                    Message = blog != null ? "Success" : "Failed",
                    Data = blog!
                });
            })
            .WithName("GetBlog")
            .WithOpenApi();

            #endregion

            #region CreateBlog

            app.MapPost("/blog", async ([FromServices] AppDbContext db, BlogModel blog,
                                [FromServices] ILogger<Program> _logger) =>
            {
                try
                {
                    await db.Blogs.AddAsync(blog);
                    int result = await db.SaveChangesAsync();

                    string message = result > 0 ? "Saving Successful." : "Saving Failed.";
                    return Results.Ok(new BlogResponseModel
                    {
                        Message = message,
                        IsSuccess = result > 0,
                        Data = blog
                    });
                }
                catch (Exception e)
                {
                    _logger.LogError("Something was wrong in saving Blog => {@e}", e);
                    throw new Exception(e.Message);
                }
            })
            .WithName("CreateBlog")
            .WithOpenApi();

            #endregion

            #region Updateblog

            app.MapPut("/blog/{id}", async ([FromServices] AppDbContext db, int id, BlogModel blog,
                                            [FromServices] ILogger<Program> _logger) =>
            {
                try
                {
                    BlogResponseModel model = new BlogResponseModel();
                    var item = await db.Blogs.Where(x => x.Blog_Id == id).FirstOrDefaultAsync();

                    if (item is null)
                    {
                        model.IsSuccess = false;
                        model.Message = "Data not found";
                        return Results.Ok(model);
                    }

                    item.Blog_Title = blog.Blog_Title;
                    item.Blog_Author = blog.Blog_Author;
                    item.Blog_Content = blog.Blog_Content;
                    int result = await db.SaveChangesAsync();

                    model.IsSuccess = result > 0;
                    model.Message = result > 0 ? "Update Successful." : "Update Failed.";
                    model.Data = item;
                    return Results.Ok(model);
                }
                catch(Exception e)
                {
                    _logger.LogError("Something was wrong in updating Blog => {@e}", e);
                    throw new Exception(e.Message);
                }
            })
            .WithName("UpdateBlog")
            .WithOpenApi();

            #endregion

            #region DeleteBlog

            app.MapDelete("/blog/{id}", async ([FromServices] AppDbContext db, int id) =>
            {
                BlogResponseModel model = new BlogResponseModel();
                var blog = await db.Blogs.Where(x => x.Blog_Id == id).FirstOrDefaultAsync();

                if (blog is null)
                {
                    model.IsSuccess = false;
                    model.Message = "Data not found";
                    return Results.Ok(model);
                }

                db.Blogs.Remove(blog);
                int result = await db.SaveChangesAsync();

                model.IsSuccess = result > 0;
                model.Message = result > 0 ? "Delete Successful." : "Delete Failed.";
                return Results.Ok(model);
            })
                .WithName("DeleteBlog")
                .WithOpenApi();

            #endregion

            #region PatchBlog

            app.MapPatch("/blog/{id}", async ([FromServices] AppDbContext db, int id, BlogModel blog) =>
            {
                BlogResponseModel model = new BlogResponseModel();
                var item = await db.Blogs.Where(x => x.Blog_Id == id).FirstOrDefaultAsync();
                if (item is null)
                {
                    model.IsSuccess = false;
                    model.Message = "Data not found.";
                    return Results.Ok(model);
                }

                if (!string.IsNullOrWhiteSpace(blog.Blog_Title))
                {
                    item.Blog_Title = blog.Blog_Title;
                }
                if (!string.IsNullOrWhiteSpace(blog.Blog_Author))
                {
                    item.Blog_Author = blog.Blog_Author;
                }
                if (!string.IsNullOrWhiteSpace(blog.Blog_Content))
                {
                    item.Blog_Content = blog.Blog_Content;
                }

                int result = await db.SaveChangesAsync();
                string message = result > 0 ? "Updating Successful." : "Updating Failed.";

                model.IsSuccess = result > 0;
                model.Message = message;
                return Results.Ok(model);
            })
                .WithName("PatchBlog")
                .WithOpenApi();

            #endregion
        }
    }
}
