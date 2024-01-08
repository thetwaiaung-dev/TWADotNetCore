using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using TWADotNetCore.ConsoleApp.Models;

namespace TWADotNetCore.ConsoleApp.DapperExamples
{
    public class DapperExample
    {
        private readonly SqlConnectionStringBuilder sqlConnectionStringBuilder = new SqlConnectionStringBuilder()
        {
            DataSource = ".",
            InitialCatalog = "AHMTZDotNetCore",
            UserID = "sa",
            Password = "thetwaiaung"
        };

        public void Run()
        {
            //Create("Dapper Title", "Dapper Author", "Dapper Content");
            //Edit(4);
            // Update(4, "Dapper Title Update", "Dapper Author Update", "Dapper Content Update");
            //Delete(3);
            Read();
        }

        public void Read()
        {
            try
            {
                string query = @"
                                select Blog_Id,Blog_Title,Blog_Author,Blog_content
                                from tbl_blog1 order by Blog_Id desc
                                ";
                using IDbConnection db = new SqlConnection(sqlConnectionStringBuilder.ConnectionString);

                List<BlogModel> blogLst = db.Query<BlogModel>(query).ToList();
                foreach(var item in blogLst)
                {
                    Console.WriteLine(item.Blog_Id);
                    Console.WriteLine(item.Blog_Title);
                    Console.WriteLine(item.Blog_Author);
                    Console.WriteLine(item.Blog_Content);
                }
            }catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Create(string title,string author,string content)
        {
            try
            {
                BlogModel blogModel = new BlogModel()
                {
                    Blog_Title = title,
                    Blog_Author = author,
                    Blog_Content = content
                };

                string query = @"
                                 insert into tbl_blog1 
                                  (Blog_Title,Blog_Author,Blog_Content) 
                                  values (@Blog_Title,@Blog_Author,@Blog_Content)
                                ";
                using IDbConnection db = new SqlConnection(sqlConnectionStringBuilder.ConnectionString);
                var result=db.Execute(query,blogModel);

                var message = result > 0 ? "Saving successful." : "Saving failed.";
                Console.WriteLine(message);
            }
            catch(Exception ex) 
            {
                throw new Exception (ex.Message);
            }
        }

        public void Edit(int id)
        {
            try
            {
                BlogModel blogModel = new BlogModel()
                {
                    Blog_Id=id
                };

                string query = @"
                               select Blog_Id,Blog_Title,Blog_Author,Blog_Content
                                from tbl_blog1 where Blog_Id=@Blog_Id
                               ";
                using IDbConnection db=new SqlConnection(sqlConnectionStringBuilder.ConnectionString);
                BlogModel resultBlog = db.Query<BlogModel>(query,blogModel).FirstOrDefault();

                Console.WriteLine(resultBlog.Blog_Id);
                Console.WriteLine(resultBlog.Blog_Title);
                Console.WriteLine(resultBlog.Blog_Author);
                Console.WriteLine(resultBlog.Blog_Content);       
            }catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Update(int id,string title,string author,string content)
        {
            try
            {
                BlogModel blogModel = new BlogModel()
                {
                    Blog_Id = id,
                    Blog_Title = title,
                    Blog_Author = author,
                    Blog_Content = content,
                };

                string query = @"
                                  update tbl_blog1 set 
                                  Blog_Title=@Blog_Title,Blog_Author=@Blog_Author,Blog_Content=@Blog_Content
                                  where Blog_Id=@Blog_Id
                                ";
                using IDbConnection db=new SqlConnection(sqlConnectionStringBuilder.ConnectionString);
                var result = db.Execute(query, blogModel);

                var message = result > 0 ? "Update successful." : "Update Failed.";
                Console.WriteLine(message);
            }catch (Exception ex) 
            {
                throw new Exception(ex.Message);
            }
        }

        public void Delete(int id)
        {
            BlogModel blogModel = new BlogModel()
            {
                Blog_Id = id,
            };
            try
            {
                string query = @"
                                    delete tbl_blog1 where Blog_Id=@Blog_Id
                                ";

                using IDbConnection db = new SqlConnection(sqlConnectionStringBuilder.ConnectionString);
                var result=db.Execute(query, blogModel);

                var message = result > 0 ? "Delete successful." : "Delete failed.";
                Console.WriteLine(message);
            }catch (Exception ex)
            {
                throw new Exception(ex.Message) ;
            }
        }
    }
}
