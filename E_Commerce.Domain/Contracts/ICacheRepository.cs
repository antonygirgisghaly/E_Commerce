using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Domain.Contracts
{
    public interface ICacheRepository
    {
        Task<string?> GetAsync(string cachekey,CancellationToken ct = default);
        Task SetAsync(string cacheKey,string cacheValue,TimeSpan? timeToLive = default,CancellationToken ct = default);
    }
}
