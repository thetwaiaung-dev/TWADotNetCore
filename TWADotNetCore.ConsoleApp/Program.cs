using System;
using System.Net.Http;
using System.Threading.Tasks;
using TWADotNetCore.ConsoleApp.ADoDotNetExamples;
using TWADotNetCore.ConsoleApp.DapperExamples;
using TWADotNetCore.ConsoleApp.EFCoreExamples;
using TWADotNetCore.ConsoleApp.HttpClientExamples;
using TWADotNetCore.ConsoleApp.RefitExamples;
using TWADotNetCore.ConsoleApp.RestClientExamples;

namespace TWADotNetCore.ConsoleApp
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            //ADoDotNetExample aDoDotNetExample = new ADoDotNetExample();
            //aDoDotNetExample.Run();

            //DapperExample dapper=new DapperExample();
            //dapper.Run();

            //EFCoreExample efCore = new EFCoreExample();
            //efCore.Run();
            Console.WriteLine("Waiting for api...");
            Console.ReadKey();

            HttpClientExample client = new HttpClientExample();
            await client.Run();

            //RestClientExample restClient = new RestClientExample();
            //await restClient.Run();

            //RefitExample refit = new RefitExample();
            //await refit.Run();
        }
    }
}
