using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace TWADotNetCore.ConsoleApp.ADoDotNetExamples
{
    public class ADoDotNetExample
    {

        private readonly SqlConnectionStringBuilder sqlConnectionStringBuilder = new SqlConnectionStringBuilder()
        {
            DataSource = ".",
            InitialCatalog = "AHMTZDotNetCore",
            UserID = "sa",
            Password = "thetwaiaung"
        };
        public  void Run()
        {
            //Create("Test1", "Author test1", "Content test1");
            //Edit(2);
            Update(2, "title update", "author update", "content update");
            Read(); 
        }

        private void Read()
        {
            try
            {
                string query = "select Blog_Id,Blog_Title,Blog_Author,Blog_Content from Tbl_Blog1 order by Blog_Id desc";
                using (var con = new SqlConnection(sqlConnectionStringBuilder.ConnectionString))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand(query, con);
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    con.Close();
                    foreach (DataRow dr in dt.Rows)
                    {
                        Console.WriteLine(dr["Blog_Id"].ToString());
                        Console.WriteLine(dr["Blog_Title"].ToString());
                        Console.WriteLine(dr["Blog_Author"].ToString());
                        Console.WriteLine(dr["Blog_Content"].ToString());
                    }
                }
            }catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private void Create(string title,string author,string content)
        {
            try
            {
                string query = @"insert into tbl_blog1
                              (Blog_Title,Blog_Author,Blog_Content)
                              values(@Blog_Title,@Blog_Author,@Blog_Content)";
                using (var con = new SqlConnection(sqlConnectionStringBuilder.ConnectionString))
                {
                    con.Open();

                    var cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@Blog_Title", title);
                    cmd.Parameters.AddWithValue("@Blog_Author", author);
                    cmd.Parameters.AddWithValue("@Blog_Content", content);
                    var result = cmd.ExecuteNonQuery();

                    string message = result > 0 ? "Saving successful." : "Saving failed.";
                    Console.WriteLine(message);

                    con.Close();

                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private void Edit(int id)
        {
            try
            {
                string query = @"select Blog_Id,Blog_Title,Blog_Author,Blog_Content
                                    from Tbl_Blog1 
                                    where Blog_Id=@Blog_Id
                                  ";
                using(var con=new SqlConnection(sqlConnectionStringBuilder.ConnectionString))
                {
                    con.Open();
                    var cmd = con.CreateCommand();
                    cmd.CommandText = query;
                    cmd.Parameters.AddWithValue("@Blog_Id", id);
                    SqlDataAdapter sqlDataAdapter= new SqlDataAdapter(cmd);

                    DataTable dt = new DataTable();
                    sqlDataAdapter.Fill(dt);

                    foreach (DataRow dr in dt.Rows)
                    {
                        Console.WriteLine(dr["Blog_Id"].ToString());
                        Console.WriteLine(dr["Blog_Title"].ToString());
                        Console.WriteLine(dr["Blog_Author"].ToString());
                        Console.WriteLine(dr["Blog_Content"].ToString());
                    }

                    con.Close();
                }
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private void Update(int id,string title,string author,string content)
        {
            int result = 0;
            try
            {
                string query = @"
                                update tbl_blog1 set 
                                 Blog_Title=@Blog_Title,Blog_Author=@Blog_Author,
                                 Blog_Content=@Blog_Content where Blog_Id=@Blog_Id
                                 ";
                using(var con=new SqlConnection(sqlConnectionStringBuilder.ConnectionString))
                {
                    con.Open();
                    var cmd = con.CreateCommand();
                    cmd.CommandText = query;
                    cmd.Parameters.AddWithValue("@Blog_Title", title);
                    cmd.Parameters.AddWithValue("@Blog_Author", author);
                    cmd.Parameters.AddWithValue("@Blog_Content", content);
                    cmd.Parameters.AddWithValue("@Blog_Id", id);
                    result = cmd.ExecuteNonQuery();

                    string message = result > 0 ? "Updating successful." : "Updating fail.";
                    Console.WriteLine(message);

                    con.Close();
                }
            }catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
