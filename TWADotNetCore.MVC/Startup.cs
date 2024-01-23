using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using TWADotNetCore.MVC.AppDbContext;
using TWADotNetCore.MVC.Interfaces;
using TWADotNetCore.MVC.Services;
using Refit;
using Serilog;
using TWADotNetCore.MVC.Helpers;
using Serilog.Sinks.MSSqlServer;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using TWADotNetCore.MVC.Models;

namespace TWADotNetCore.MVC
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();

            services.AddDbContext<BlogDbContext>(opt =>
            {
                opt.UseSqlServer(Configuration.GetConnectionString("con"));
            }, ServiceLifetime.Transient, ServiceLifetime.Transient);

            #region HttpClient

            services.AddScoped(x => new HttpClient
            {
                BaseAddress = new Uri(Configuration.GetSection("RestApiUrl").Value)
            });

            #endregion

            #region RestClient

            services.AddScoped(x => new RestClient(Configuration.GetSection("RestApiUrl").Value));

            #endregion

            #region Refit

            services
            .AddRefitClient<IBlogApi>(
                new RefitSettings(new NewtonsoftJsonContentSerializer
                (new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() }))
                )
            .ConfigureHttpClient(c => c.BaseAddress = new Uri(Configuration.GetSection("RestApiUrl").Value));

            #endregion

            #region Custom Setting from json

            services.AddOptions();
            services.Configure<CustomAppSettingModel>(Configuration.GetSection("CustomSetting"));

            #endregion

            #region Injection
            services.AddTransient<ReportService>();
            services.AddTransient<BlogService>();
            services.AddTransient<LogHelper>();
            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            #region Serilog
            app.UseSerilogRequestLogging();

            Log.Logger = new LoggerConfiguration()
                                .WriteTo.File("D:/MVCLogs/MVCLog.txt", rollingInterval: RollingInterval.Hour)
                                //.WriteTo
                                //.MSSqlServer(
                                //        connectionString: "Data Source=.;Initial Catalog=AHMTZDotNetCore;User ID=sa;Password=thetwaiaung;TrustServerCertificate=True;",
                                //        sinkOptions: new MSSqlServerSinkOptions
                                //        {
                                //            TableName = "LogEvents",
                                //            AutoCreateSqlTable = true,
                                //        })
                                .CreateLogger();

            #endregion

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
