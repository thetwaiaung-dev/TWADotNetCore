using AspNetCoreRateLimit;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using System.Threading.RateLimiting;
using TWADotNetCore.RestApi.EFCoreExamples;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

#region API limit request 
builder.Services.AddMemoryCache();
builder.Services.Configure<IpRateLimitOptions>(options =>
{
    options.EnableEndpointRateLimiting = true;
    options.StackBlockedRequests = false;
    options.HttpStatusCode = 429;
    options.RealIpHeader = "X-Real-IP";
    options.ClientIdHeader = "X_ClientId";
    options.QuotaExceededMessage = "Too many requests.Please wait..";
    options.GeneralRules = new List<RateLimitRule>
    {
        new RateLimitRule
        {
            Endpoint="get:/api/blog",
            Period="20s",
            Limit=2,
        },
        new RateLimitRule
        {
            Endpoint="get:/api/blog/*",
            Period="20",
            Limit=2,
        }
    };
});
builder.Services.AddSingleton<IIpPolicyStore, MemoryCacheIpPolicyStore>();
builder.Services.AddSingleton<IRateLimitCounterStore, MemoryCacheRateLimitCounterStore>();
builder.Services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();
builder.Services.AddSingleton<IProcessingStrategy, AsyncKeyLockProcessingStrategy>();
builder.Services.AddInMemoryRateLimiting();
#endregion

#region API limit request 2
//builder.Services.AddRateLimiter(rateLimitOption =>
//    rateLimitOption.AddFixedWindowLimiter(policyName: "fixed", op =>
//    {
//        op.PermitLimit = 2;
//        op.Window=TimeSpan.FromSeconds(30);
//        op.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
//        op.QueueLimit = 2;
//    })
//);
#endregion

#region API limit request 3
builder.Services.AddOptions();
builder.Services.AddMemoryCache();

builder.Services.Configure<IpRateLimitOptions>(builder.Configuration.GetSection("IpRateLimiting"));

builder.Services.AddInMemoryRateLimiting();

builder.Services.AddTransient<IRateLimitConfiguration, RateLimitConfiguration>();
#endregion

builder.Services.AddControllers().AddJsonOptions(opt =>
{
    opt.JsonSerializerOptions.PropertyNamingPolicy = null;
    //opt.JsonSerializerOptions.PropertyNameCaseInsensitive = false;
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<IConfiguration>(op =>
{
    op.GetSection("ConnectionStrings");
});

builder.Services.AddDbContext<AppDbContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("con"));
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

/* limit request */
//app.UseIpRateLimiting();
/* end */

/* limit request 2 */
//app.UseRateLimiter();
/* end */

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

