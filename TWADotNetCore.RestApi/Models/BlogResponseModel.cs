namespace TWADotNetCore.RestApi.Models
{
    public class BlogResponseModel
    {
        public bool IsSuccess { get; set; }
        public string  Message { get; set; }
        public BlogModel Data { get; set; }
    }

    public class BlogListResponseModel
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public List<BlogModel> Data { get; set; }
    }
}
