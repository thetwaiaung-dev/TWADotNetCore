using System.Collections.Generic;

namespace TWADotNetCore.MVC.Models
{
    public class ResponseModel
    {
    }

    public class BlogResponseModel
    {
        public BlogResponseModel()
        {

        }

        public BlogResponseModel(BlogModel blog, MessageModel message)
        {
            this.Blog = blog;
            this.Message = message;
        }

        public BlogModel Blog { get; set; }

        public PageSettingModel PageSetting { get; set; }

        public MessageModel Message { get; set; }
    }

    public class BlogListResponseModel
    {
        public List<BlogModel> BlogList { get; set; }

        public PageSettingModel PageSetting { get; set; }

        public MessageModel Message { get; set; }
    }

    public class MessageModel
    {
        public bool IsSuccess { get; set; }

        public string Message { get; set; }
    }

    public class BlogListApiResponseModel : MessageModel
    {
        public List<BlogModel> Data { get; set; }
    }

    public class BlogApiResponseModel : MessageModel
    {
        public BlogModel Data { get; set; }
    }
}
