using E_Commerce.Application.Comman;
using E_Commerce.Application.DTOs;
using E_Commerce.Application.DTOs.Basket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application.Contracts
{
    public interface IBasketService
    {
        Task<Result<BasketDto>> GetBasketAsync(string basketId, CancellationToken ct = default);
        Task<Result<BasketDto>> CreateOrUpdateAsync(BasketDto basket, TimeSpan? timeToLive = default, CancellationToken ct = default);
        Task<Result<bool>> DeleteBasketAsync(string basketId, CancellationToken ct = default);
    }
}
