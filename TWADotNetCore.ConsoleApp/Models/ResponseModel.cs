using System;
using System.Collections.Generic;
using System.Text;

namespace TWADotNetCore.ConsoleApp.Models
{
    public class ResponseModel
    {

    }

    public class BlogResponseModel
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public BlogModel Data { get; set; }
    }

    public class BlogListResponseModel
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public List<BlogModel> Data { get; set; }

        public static implicit operator BlogListResponseModel(BlogModel v)
        {
            throw new NotImplementedException();
        }
    }
}
