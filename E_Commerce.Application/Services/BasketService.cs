using AutoMapper;
using E_Commerce.Application.Comman;
using E_Commerce.Application.Contracts;
using E_Commerce.Application.DTOs.Basket;
using E_Commerce.Domain.Contracts;
using E_Commerce.Domain.Entities.Baskets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application.Services
{
    internal class BasketService : IBasketService
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IMapper _mapper;

        public BasketService(IBasketRepository basketRepository,IMapper mapper)
        {
            _basketRepository = basketRepository;
            _mapper = mapper;
        }
        public async Task<Result<BasketDto>> CreateOrUpdateAsync(BasketDto basket, TimeSpan? timeToLive = null, CancellationToken ct = default)
        {
            var map = _mapper.Map<CustomerBasket>(basket);
            var result = await _basketRepository.CreateOrUpdateBasketAsync(map, timeToLive, ct);
            return result == null ? Result<BasketDto>.Fail(Error.Failure("BasketCreate.Failure","Cannot create or update basket")):
                Result<BasketDto>.Ok(basket);
        }

        public async Task<Result<bool>> DeleteBasketAsync(string basketId, CancellationToken ct = default)
        {
          var result = await _basketRepository.DeleteBasketAsync(basketId,ct);
            return result ? Result<bool>.Ok(true) : Result<bool>.Fail(Error.Failure("BasketDelete.Failure","Cannot Delte basket"));
        }

        public async Task<Result<BasketDto>> GetBasketAsync(string basketId, CancellationToken ct = default)
        {
            var basket = await _basketRepository.GetBasketAsync(basketId,ct);
            return basket == null ? Result<BasketDto>.Fail(Error.NotFound("Basket.NotFound", $"Basket with {basketId} Not Found")) :
                Result<BasketDto>.Ok(_mapper.Map<BasketDto>(basket));

        }
    }
}
