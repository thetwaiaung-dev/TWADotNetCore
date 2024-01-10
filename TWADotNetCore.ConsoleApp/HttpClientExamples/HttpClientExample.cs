using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TWADotNetCore.ConsoleApp.Models;
using static System.Net.Mime.MediaTypeNames;

namespace TWADotNetCore.ConsoleApp.HttpClientExamples
{
    public class HttpClientExample
    {
        public async Task Run()
        {
            //await Read();
            //await Edit(8);

            //await Create("Rest api title3", "Rest author3", "Rest content3");
            await Update(9,"Rest api title update", "Rest author update", "Rest content3");
        }

        public async Task Read()
        {
            HttpClient client = new HttpClient();
            var response = await client.GetAsync("https://localhost:7001/api/Blog");

            if (response.IsSuccessStatusCode)
            {
                string jsonStr = await response.Content.ReadAsStringAsync();
                BlogListResponseModel model = JsonConvert.DeserializeObject<BlogListResponseModel>(jsonStr);

                Console.WriteLine(JsonConvert.SerializeObject(model, Formatting.Indented));
            }
        }

        public async Task Edit(int id)
        {
            HttpClient client = new HttpClient();
            var response = await client.GetAsync($"https://localhost:7001/api/Blog/{id}");

            if (response.IsSuccessStatusCode)
            {
                string jsonStr = await response.Content.ReadAsStringAsync();
                BlogResponseModel model = JsonConvert.DeserializeObject<BlogResponseModel>(jsonStr);

                Console.WriteLine(JsonConvert.SerializeObject(model, Formatting.Indented));
            }
        }

        public async Task Create(string title, string author, string content)
        {
            BlogModel blog = new BlogModel()
            {
                Blog_Title = title,
                Blog_Author = author,
                Blog_Content = content
            };

            string blogJson = JsonConvert.SerializeObject(blog);
            HttpContent httpContent = new StringContent(blogJson, Encoding.UTF8,Application.Json);

            HttpClient client = new HttpClient();
            var response = await client.PostAsync("https://localhost:7001/api/Blog/",httpContent);

            if (response.IsSuccessStatusCode)
            {
                string jsonStr= await response.Content.ReadAsStringAsync();
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
            string blogJson=JsonConvert.SerializeObject(blog);

            HttpContent httpContent=new StringContent(blogJson,Encoding.UTF8,Application.Json);

            HttpClient client=new HttpClient();
            var response = await client.PutAsync($"https://localhost:7001/api/Blog/{id}", httpContent);

            if (response.IsSuccessStatusCode)
            {
                string jsonStr=await response.Content.ReadAsStringAsync();
                BlogResponseModel model = JsonConvert.DeserializeObject<BlogResponseModel>(jsonStr);

                Console.WriteLine(JsonConvert.SerializeObject(model,Formatting.Indented));
            }
        }

        public async Task Delete(int id)
        {
            HttpClient client = new HttpClient();
            var response = await client.DeleteAsync($"https://localhost:7001/api/Blog/{id}");

            if (response.IsSuccessStatusCode)
            {
                string jsonStr = await response.Content.ReadAsStringAsync();
                BlogResponseModel model = JsonConvert.DeserializeObject<BlogResponseModel>(jsonStr);

                Console.WriteLine(JsonConvert.SerializeObject(model, Formatting.Indented));
            }
        }

    }
}
