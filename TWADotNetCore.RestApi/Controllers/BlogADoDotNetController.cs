using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using TWADotNetCore.RestApi.Dtos;
using TWADotNetCore.RestApi.Models;

namespace TWADotNetCore.RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogADoDotNetController : ControllerBase
    {
        private readonly SqlConnectionStringBuilder _sqlConnnectionString;

        public BlogADoDotNetController(IConfiguration configuration)
        {
            string con = configuration.GetConnectionString("con");
            _sqlConnnectionString = new SqlConnectionStringBuilder(con);
        }

        [HttpGet]
        public IActionResult GetBlogs()
        {
            string query = @"
                                select Blog_Id,Blog_Title,Blog_Author,Blog_content
                                from tbl_blog1 order by Blog_Id desc
                                ";

            using SqlConnection con = new SqlConnection(_sqlConnnectionString.ConnectionString);
            con.Open();

            SqlCommand cmd = con.CreateCommand();
            cmd.CommandText = query;

            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);

            con.Close();

            List<BlogModel> blogs = new List<BlogModel>();
            foreach (DataRow dr in dt.Rows)
            {
                BlogModel blog = new BlogModel();
                blog.Blog_Id = Convert.ToInt32(dr["Blog_Id"]);
                blog.Blog_Title = dr["Blog_Title"].ToString();
                blog.Blog_Author = dr["Blog_Author"].ToString();
                blog.Blog_Content = dr["Blog_Content"].ToString();

                blogs.Add(blog);
            }

            BlogListResponseModel model = new BlogListResponseModel();
            if (blogs.IsNullOrEmpty())
            {
                model.IsSuccess = false;
                model.Message = "Failed";
                return NotFound(model);
            }

            model.IsSuccess = true;
            model.Message = "Success";
            model.Data = blogs;

            return Ok(model);
        }

        [HttpGet("{id}")]
        public IActionResult GetBlog(int id)
        {
            string query = @"select Blog_Id,Blog_Title,Blog_Author,Blog_Content
                                    from Tbl_Blog1 
                                    where Blog_Id=@Blog_Id
                                  ";

            using var con = new SqlConnection(_sqlConnnectionString.ConnectionString);
            con.Open();

            SqlCommand cmd = con.CreateCommand();
            cmd.CommandText = query;
            cmd.Parameters.AddWithValue("@Blog_Id", id);

            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);

            BlogResponseModel model = new BlogResponseModel();

            if (dt.Rows.Count == 0)
            {
                model.IsSuccess = false;
                model.Message = "Failed";
                return NotFound(model);
            }

            BlogModel blog = new BlogModel();
            foreach (DataRow dr in dt.Rows)
            {
                blog.Blog_Id = Convert.ToInt32(dr["Blog_Id"]);
                blog.Blog_Title = dr["Blog_Title"].ToString();
                blog.Blog_Author = dr["Blog_Author"].ToString();
                blog.Blog_Content = dr["Blog_Content"].ToString();
            }

            model.IsSuccess = true;
            model.Message = "Success";
            model.Data = blog;

            return Ok(model);
        }

        [HttpPost]
        public IActionResult Create([FromBody] BlogDto dto)
        {
            string query = @"insert into tbl_blog1
                              (Blog_Title,Blog_Author,Blog_Content)
                              values(@Blog_Title,@Blog_Author,@Blog_Content)";

            using var con = new SqlConnection(_sqlConnnectionString.ConnectionString);
            con.Open();

            var cmd = con.CreateCommand();
            cmd.CommandText = query;
            cmd.Parameters.AddWithValue("@Blog_Title", dto.BlogTitle);
            cmd.Parameters.AddWithValue("@Blog_Author", dto.BlogAuthor);
            cmd.Parameters.AddWithValue("@Blog_Content", dto.BlogContent);

            var result = cmd.ExecuteNonQuery();
            con.Close();

            string message = result > 0 ? "Success" : "Failed";

            BlogResponseModel model = new BlogResponseModel();
            model.IsSuccess = result > 0;
            model.Message = message;

            return Ok(model);
        }

        [HttpPut]
        public IActionResult Update([FromBody] BlogDto dto)
        {
            string query = @"
                                update tbl_blog1 set 
                                 Blog_Title=@Blog_Title,Blog_Author=@Blog_Author,
                                 Blog_Content=@Blog_Content where Blog_Id=@Blog_Id
                                 ";

            using var con=new SqlConnection(_sqlConnnectionString.ConnectionString);
            con.Open();

            var cmd = con.CreateCommand();
            cmd.CommandText= query;
            cmd.Parameters.AddWithValue("@Blog_Title", dto.BlogTitle);
            cmd.Parameters.AddWithValue("@Blog_Author", dto.BlogAuthor);
            cmd.Parameters.AddWithValue("@Blog_Content", dto.BlogContent);
            cmd.Parameters.AddWithValue("@Blog_Id", dto.Blog_Id);

            var result = cmd.ExecuteNonQuery();
            con.Close();

            string message = result > 0 ? "Success" : "Failed";
            BlogResponseModel model = new BlogResponseModel();
            model.IsSuccess = result > 0;
            model.Message= message;

            return Ok(model);
        }
    }
}
