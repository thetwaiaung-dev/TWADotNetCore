using TWADotNetCore.MVC.Dtos;

namespace TWADotNetCore.MVC.Models
{
    public static class ChangeModel
    {
        public static BlogDto Change(this BlogModel model)
        {
            if (model == null) return null;

            return new BlogDto()
            {
                Blog_Id = model.Blog_Id,
                Blog_Title = model.Blog_Title,
                Blog_Author = model.Blog_Author,
                Blog_Content = model.Blog_Content,
            };
        }

        public static BlogModel Change(this BlogDto dto)
        {
            if(dto == null) return null;

            return new BlogModel()
            {
                Blog_Id = dto.Blog_Id,
                Blog_Title = dto.Blog_Title,
                Blog_Author = dto.Blog_Author,
                Blog_Content = dto.Blog_Content,
            };
        }
    }
}
