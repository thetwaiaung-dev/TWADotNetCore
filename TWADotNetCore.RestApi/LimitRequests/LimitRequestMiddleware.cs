using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;

namespace TWADotNetCore.RestApi.LimitRequests
{
    public class LimitRequestMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly MemoryDistributedCache _cache;

        private static string GenerateClientKey(HttpContext context) =>
            $"{context.Request.Path}_{context.Connection.RemoteIpAddress}";

        public LimitRequestMiddleware(RequestDelegate next,MemoryDistributedCache cache)
        {
            _next = next;
            _cache=cache;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var endPoint = context.GetEndpoint();
            var decorator = endPoint?.Metadata.GetMetadata<LimitRequest>();

            if (decorator is null)
            {
                await _next(context);
                return;
            }

            var key = GenerateClientKey(context);
            //await 
            //var clientStatistics=await Getclien
        }

        //private async Task<ClientStatistics> GetClientStatisticsByKey(string key)
        //{
        //    return await _cache.get
        //}
    }
}
