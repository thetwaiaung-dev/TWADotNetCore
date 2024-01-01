using System;
using System.Collections.Generic;
using System.Text;
using TWADotNetCore.Dtos;
using TWADotNetCore.Models;

namespace TWADotNetCore.ConsoleApp.Changes
{
    public static class ChangeModel
    {
        public static BlogModel Change(this BlogDto dto)
        {
            if (dto == null) return null;
            BlogModel model= new BlogModel() 
            {
                Blog_Id=dto.Blog_Id,
                Blog_Title=dto.Blog_Title,
                Blog_Author=dto.Blog_Author,
                Blog_Content=dto.Blog_Content,
            };
            return model;
        }

        public static BlogDto Change(this BlogModel model)
        {
            if(model==null) return null;
            BlogDto dto = new BlogDto()
            {
                Blog_Id= model.Blog_Id,
                Blog_Title= model.Blog_Title,
                Blog_Author= model.Blog_Author, 
                Blog_Content =model.Blog_Content,
            };
            return dto;
        }
    }
}
