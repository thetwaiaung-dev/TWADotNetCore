using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace TWADotNetCore.MVC.Models
{
    [Table("Tbl_Blog1")]
    public class BlogModel
    {
        [Key]
        public int Blog_Id { get; set; }
        public string Blog_Title { get; set; }
        public string Blog_Content { get; set; }
        public string Blog_Author { get; set; }
    }

    public class BlogDataResponseModel
    {
        public BlogModel Blog { get; set; }
        public PageSettingModel PageSetting { get; set; }
    }

    public class BlogDataListResponseModel
    {
        public List<BlogModel> BlogList { get; set; }
        public PageSettingModel PageSetting { get; set; }
    }

}
