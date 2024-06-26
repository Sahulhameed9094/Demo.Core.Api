﻿using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace Demo.Core.Api.Cache
{
    public static class DistributedCacheExtension
    {
        public static async Task SetRecordAsync<T>(
            this IDistributedCache cache,
            string recodeId,
            T data,
            TimeSpan? absoluteExpireTime = null,
            TimeSpan? slidingExpirationTime = null)
        {
            var options = new DistributedCacheEntryOptions();
            options.AbsoluteExpirationRelativeToNow = absoluteExpireTime ?? TimeSpan.FromSeconds(60);
            options.SlidingExpiration = slidingExpirationTime;
            var jsonData = JsonSerializer.Serialize(data);
            await cache.SetStringAsync(recodeId, jsonData, options);
        }
        public static async Task<T> GetRecordAsync<T>(this IDistributedCache cache, string recordId)
        {
            var jsonData = await cache.GetStringAsync(recordId);
            if (jsonData is null)
            {
                return default(T);
            }
            return JsonSerializer.Deserialize<T>(jsonData);
        }
    }
}
