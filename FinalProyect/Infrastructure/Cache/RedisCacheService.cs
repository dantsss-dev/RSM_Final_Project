using System.Text.Json;
using FinalProyect.Domain.Interface;
using Microsoft.Extensions.Caching.Distributed;

namespace FinalProyect.Infrastructure.Cache
{
    public class RedisCacheService : IRedisCacheService
    {
        private readonly IDistributedCache _cache;
        public RedisCacheService(IDistributedCache cache)
        {
            _cache = cache;   
        }
        public  async Task SetRecordAsync<T>( string recordId, T data, TimeSpan? absoluteExpireTime = null, TimeSpan? unusuedExpireTime = null)
        {
            var options = new DistributedCacheEntryOptions();

            options.AbsoluteExpirationRelativeToNow = absoluteExpireTime ?? TimeSpan.FromSeconds(60);
            options.SlidingExpiration = unusuedExpireTime;

            var jsonData = JsonSerializer.Serialize(data);
            await _cache.SetStringAsync(recordId, jsonData, options);
        }
        
        public async Task<T?> GetRecordAsync<T>(string recordId)
        {
            var jsonData = await _cache.GetStringAsync(recordId);

            if(jsonData is null)
            {
                return default;
            }

            return JsonSerializer.Deserialize<T>(jsonData);
        }
    }
}