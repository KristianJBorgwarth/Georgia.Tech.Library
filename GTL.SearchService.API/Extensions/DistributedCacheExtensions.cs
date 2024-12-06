using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace GTL.SearchService.API.Extensions
{
    public static class DistributedCacheExtensions
    {
        //Dealing with Redis
        //Set records in cache generic
        public static async Task SetRecordAsync<T>(this IDistributedCache cache,
            string recordId, //The Key, unique identifier
            T data, //Value - bog detaljer
            TimeSpan? absoluteExpireTime = null, //autooprydning efter tidsspan
            TimeSpan? unusedExpireTime = null) //Hvis cachen ikke anvendes i tidsrum => slet cache (overruler absoluteExpireTime)
        {
            var options = new DistributedCacheEntryOptions();

            options.AbsoluteExpirationRelativeToNow = absoluteExpireTime ?? TimeSpan.FromSeconds(60); //Sættter udløbstiden til default, hvis ikke den er sat...
            options.SlidingExpiration = unusedExpireTime; //default null... Betyder at den ikke er sat. 

            var jsonData = JsonSerializer.Serialize(data); //Bog detaljerne serialiseres til JSON
            await cache.SetStringAsync(recordId, jsonData, options);
        }

        //Get reads from Redis generic method
        public static async Task<T> GetRecordAsync<T>(this IDistributedCache cache, string recordId)
        {
            var jsonData = await cache.GetStringAsync(recordId); // Henter værdi på værdi

            if (jsonData == null)
            {
                return default(T);
            }

            return JsonSerializer.Deserialize<T>(jsonData);
        }

    }
}