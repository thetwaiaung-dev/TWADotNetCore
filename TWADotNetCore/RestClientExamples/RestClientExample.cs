using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TWADotNetCore.ConsoleApp.Models;

namespace TWADotNetCore.ConsoleApp.RestClientExamples
{
    public class RestClientExample
    {
        public async Task Run()
        {
            //await Edit(11);

            //await Create("Restclient Title", "Restclient Author", "Restclient Content");

            //await Update(12,"Restclient Title update", "Restclient Author update", "Restclient Content update");

            //await Delete(11);

            await Read();
        }

        public async Task Read()
        {
            RestClient client = new RestClient();
            RestRequest request = new RestRequest("https://localhost:7001/api/Blog", Method.Get);

            //await client.GetAsync(request);
            var response = await client.ExecuteAsync(request);
            if (response.IsSuccessStatusCode)
            {
                string jsonStr=response.Content;
                BlogListResponseModel model=JsonConvert.DeserializeObject<BlogListResponseModel>(jsonStr);  
                Console.WriteLine(JsonConvert.SerializeObject(model,Formatting.Indented));
            }
        }  

        public async Task Edit(int id)
        {
            RestClient client=new RestClient();
            RestRequest request = new RestRequest($"https://localhost:7001/api/Blog/{id}");

            var response= await client.ExecuteAsync(request);
            if (response.IsSuccessStatusCode)
            {
                string jsonStr = response.Content;
                BlogResponseModel model = JsonConvert.DeserializeObject<BlogResponseModel>(jsonStr);

                Console.WriteLine(JsonConvert.SerializeObject(model,Formatting.Indented));
            }
        }

        public async Task Create(string title,string author,string content)
        {
            BlogModel blog = new BlogModel()
            {
                Blog_Title = title,
                Blog_Author = author,
                Blog_Content = content
            };

            RestClient client = new RestClient();
            RestRequest request = new RestRequest("https://localhost:7001/api/Blog/", Method.Post);
            request.AddBody(blog);  

            var response= await client.ExecuteAsync(request);
            if (response.IsSuccessStatusCode)
            {
                string jsonStr = response.Content;
                BlogResponseModel model=JsonConvert.DeserializeObject<BlogResponseModel>(jsonStr);

                Console.WriteLine(JsonConvert.SerializeObject(model, Formatting.Indented));
            }
        }

        public async Task Update(int id,string title,string author,string content)
        {
            BlogModel blog = new BlogModel()
            {
                Blog_Title = title,
                Blog_Author = author,
                Blog_Content = content
            };

            RestClient client = new RestClient();
            RestRequest request = new RestRequest($"https://localhost:7001/api/Blog/{id}",Method.Put);
            request.AddBody(blog);

            var response=await client.ExecuteAsync(request);    
            if (response.IsSuccessStatusCode)
            {
                string jsonStr= response.Content;
                BlogResponseModel model = JsonConvert.DeserializeObject<BlogResponseModel>(jsonStr);

                Console.WriteLine(JsonConvert.SerializeObject(model,Formatting.Indented));
            }
        }

        public async Task Delete(int id)
        {
            RestClient client = new RestClient();
            RestRequest request = new RestRequest($"https://localhost:7001/api/Blog/{id}", Method.Delete);

            var response=await client.ExecuteAsync(request);
            if (response.IsSuccessStatusCode)
            {
                string jsonStr = response.Content;
                BlogResponseModel model = JsonConvert.DeserializeObject<BlogResponseModel>(jsonStr);

                Console.WriteLine(JsonConvert.SerializeObject(model, Formatting.Indented));
            }
        }
    }
}
