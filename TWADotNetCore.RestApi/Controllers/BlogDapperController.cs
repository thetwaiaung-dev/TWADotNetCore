using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using System.Data;
using TWADotNetCore.RestApi.Dtos;
using TWADotNetCore.RestApi.Models;

namespace TWADotNetCore.RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogDapperController : ControllerBase
    {
        private readonly SqlConnectionStringBuilder _sqlConnectionStringBuilder;

        public BlogDapperController(IConfiguration configuration)
        {
            string con = configuration.GetConnectionString("con");
            _sqlConnectionStringBuilder = new SqlConnectionStringBuilder(con);
        }

        [HttpGet]
        public IActionResult GetBlogs()
        {
            string query = @"
                                select Blog_Id,Blog_Title,Blog_Author,Blog_content
                                from tbl_blog1 order by Blog_Id desc
                                ";
            using IDbConnection db = new SqlConnection(_sqlConnectionStringBuilder.ConnectionString);

            List<BlogModel> blogLst = db.Query<BlogModel>(query).ToList();
            BlogListResponseModel model=new BlogListResponseModel() 
            {
                IsSuccess=true,
                Message="Success",
                Data=blogLst
            };

            return Ok(model);
        }

        [HttpGet("{id}")]
        public IActionResult EditBlog(int id)
        {
            BlogModel blogUpdate = new BlogModel()
            {
                Blog_Id = id,
            };

            string query = @"
                               select Blog_Id,Blog_Title,Blog_Author,Blog_Content
                                from tbl_blog1 where Blog_Id=@Blog_Id
                               ";
            using IDbConnection db=new SqlConnection(_sqlConnectionStringBuilder.ConnectionString);

            BlogModel blog = db.Query<BlogModel>(query, blogUpdate).FirstOrDefault();

            BlogResponseModel model = new BlogResponseModel();
            if (blog == null)
            {
                model.IsSuccess = false;
                model.Message = "Failed";
                return NotFound(model);
            }

            model.IsSuccess = true;
            model.Message = "Success";
            model.Data = blog;

            return Ok(model);   
        }

        [HttpPost]
        public IActionResult Create([FromBody] BlogDto dto)
        {
            BlogModel blog = new BlogModel()
            {
                Blog_Id = dto.Blog_Id,
                Blog_Title = dto.BlogTitle,
                Blog_Author = dto.BlogAuthor,
                Blog_Content = dto.BlogContent
            };

            string query = @"
                                 insert into tbl_blog1 
                                  (Blog_Title,Blog_Author,Blog_Content) 
                                  values (@Blog_Title,@Blog_Author,@Blog_Content)
                                ";
            
            using IDbConnection db=new SqlConnection(_sqlConnectionStringBuilder.ConnectionString);

            var result = db.Execute(query, blog);

            string message = result > 0 ? "Success" : "Failed";

            BlogResponseModel model=new BlogResponseModel() 
            {
                IsSuccess=result>0,
                Message = message,
            };
            return Ok(model);
        }

        [HttpPut]
        public IActionResult Update([FromBody] BlogDto dto)
        {
            BlogModel blogUpdate = new BlogModel()
            {
                Blog_Id = dto.Blog_Id,
                Blog_Title = dto.BlogTitle,
                Blog_Author = dto.BlogAuthor,
                Blog_Content = dto.BlogContent
            };

            string query = @"
                                  update tbl_blog1 set 
                                  Blog_Title=@Blog_Title,Blog_Author=@Blog_Author,Blog_Content=@Blog_Content
                                  where Blog_Id=@Blog_Id
                                ";

            using IDbConnection db= new SqlConnection(_sqlConnectionStringBuilder.ConnectionString);    

            var result=db.Execute(query, blogUpdate);

            string message = result > 0 ? "Success" : "Failed";

            BlogResponseModel model = new BlogResponseModel()
            {
                IsSuccess = result > 0,
                Message = message,
            };

            return Ok(model);
        }
    }
}
