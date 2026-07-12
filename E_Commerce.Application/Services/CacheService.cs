using E_Commerce.Application.Contracts;
using E_Commerce.Domain.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace E_Commerce.Application.Services
{
    public class CacheService : ICacheService
    {
        private readonly ICacheRepository _cacheRepository;

        public CacheService(ICacheRepository cacheRepository)
        {
            _cacheRepository = cacheRepository;
        }

        public async Task<string?> GetDataAsync(string cacheKey, CancellationToken ct = default)
        {
           var result = await _cacheRepository.GetAsync(cacheKey, ct);
            return result;
        }

        public Task SetDataAsync(string cacheKey, object value, TimeSpan? timeToLive = null, CancellationToken ct = default)
        {
            var jsonValue = JsonSerializer.Serialize(value, new JsonSerializerOptions() {

                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });
            return _cacheRepository.SetAsync(cacheKey, jsonValue, timeToLive, ct);
        }
    }
}
