using System.ComponentModel.DataAnnotations;

namespace TWADotNetCore.MVC.Dtos
{
    public class BlogDto
    {
        public int Blog_Id { get; set; }
        [Required(ErrorMessage ="Please enter Blog Title.")]
        public string Blog_Title { get; set; }
        [Required(ErrorMessage = "Please enter Blog Author.")]
        public string Blog_Content { get; set; }
        [Required(ErrorMessage = "Please enter Blog Content.")]
        public string Blog_Author { get; set; }
    }
}
