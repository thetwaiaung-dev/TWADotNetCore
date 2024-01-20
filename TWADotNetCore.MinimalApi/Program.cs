using Microsoft.EntityFrameworkCore;
using Serilog;
using Serilog.Sinks.MSSqlServer;
using System.Text.Json.Serialization;
using TWADotNetCore.MinimalApi;
using TWADotNetCore.MinimalApi.features.Blog;

Log.Logger = new LoggerConfiguration()
                    .WriteTo.Console()
                    .WriteTo.File("logs/apiLog.txt", rollingInterval: RollingInterval.Hour)
                    .WriteTo
                        .MSSqlServer(
                                connectionString: "Data Source=.;Initial Catalog=AHMTZDotNetCore;User ID=sa;Password=thetwaiaung;TrustServerCertificate=True;",
                                sinkOptions: new MSSqlServerSinkOptions
                                {
                                    TableName = "LogEvents",
                                    AutoCreateSqlTable = true,
                                })
                    .CreateLogger();

try
{
    Log.Information("Application Started...");
    var builder = WebApplication.CreateBuilder(args);

    builder.Host.UseSerilog();

    // Add services to the container.
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    //ignore json case property
    builder.Services.ConfigureHttpJsonOptions(opt =>
    {
        opt.SerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
        opt.SerializerOptions.PropertyNamingPolicy = null;
    });

    builder.Services.AddDbContext<AppDbContext>(opt =>
    {
        opt.UseSqlServer(builder.Configuration.GetConnectionString("DbConnection"));
    },
    ServiceLifetime.Transient,
    ServiceLifetime.Transient);

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseSerilogRequestLogging();

    app.UseHttpsRedirection();

    app.AddBlogService();

    app.Run();

}
catch (Exception e)
{
    Log.Fatal("Application terminated . The Error is => {@e}", e);
}
finally
{
    Log.CloseAndFlush();
}

