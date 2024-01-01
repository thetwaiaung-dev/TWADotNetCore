using System;
using System.Collections.Generic;
using System.Text;

namespace TWADotNetCore.ConsoleApp.Dtos
{
    public class BlogDto
    {
        public int Blog_Id { get; set; }
        public string Blog_Title { get; set; }
        public string Blog_Content { get; set; }
        public string Blog_Author { get; set; }
    }
}
