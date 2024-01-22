
using Microsoft.Extensions.Logging;
using NLog;
using NLog.Web;

var logger = NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();
//LogManager.Setup().LoadConfiguration(builder =>
//{
//    builder.ForLogger()
//            .FilterMinLevel(NLog.LogLevel.Debug)
//            .WriteToFile(fileName: "D:/TWADotNetCoreNlog/nlog_${shortdate}.txt");
//});

//var logger = LogManager.GetCurrentClassLogger();

//var databaseTarget = new d
//databaseTarget.CommandText = "insert into dbo.Log (Message) values (@Message)";
//databaseTarget.CommandType = System.Data.CommandType.Text;
//databaseTarget.ConnectionString = @"server=.\SQLEXPRESS;database=CiPetPet;integrated security=true;";
//databaseTarget.DBProvider = "sqlserver";
//databaseTarget.Parameters.Add(new DatabaseParameterInfo("@Message", "${message}"));
try
{
    logger.Info("Application started...");

    var builder = WebApplication.CreateBuilder(args);

    builder.Host.UseNLog();

    // Add services to the container.
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    var app = builder.Build();


    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }
    app.UseHttpsRedirection();

    var summaries = new[]
    {
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

    app.MapGet("/weatherforecast", () =>
    {
        var forecast = Enumerable.Range(1, 5).Select(index =>
            new WeatherForecast
            (
                DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                Random.Shared.Next(-20, 55),
                summaries[Random.Shared.Next(summaries.Length)]
            ))
            .ToArray();
        return forecast;
    })
    .WithName("GetWeatherForecast")
    .WithOpenApi();

    app.Run();
}
catch (Exception e)
{
    logger.Error("Application started fail..");
}

internal record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
