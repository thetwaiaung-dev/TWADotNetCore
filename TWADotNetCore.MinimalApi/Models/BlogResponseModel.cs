namespace TWADotNetCore.MinimalApi.Models
{
    public class BlogResponseModel : MessageResponseModel
    {
        public BlogModel Data { get; set; }
    }

    public class BlogListResponseModel : MessageResponseModel
    {
        public List<BlogModel> Data { get; set; }
    }

    public class MessageResponseModel
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
    }
}
