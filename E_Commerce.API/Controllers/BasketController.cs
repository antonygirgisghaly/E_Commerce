using E_Commerce.Application.Contracts;
using E_Commerce.Application.DTOs.Basket;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.API.Controllers
{
    public class BasketController : ApiBaseController
    {
        private readonly IBasketService _basketService;

        public BasketController(IBasketService basketService)
        {
            _basketService = basketService;
        }

        [HttpGet("{basketId}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(BasketDto), StatusCodes.Status200OK)]

        public async Task<ActionResult<BasketDto>> GetBasket(string basketId, CancellationToken ct)
        {
            var result = await _basketService.GetBasketAsync(basketId, ct);
            return ToActionResult(result);
        }

        [HttpPost]
        public async Task<ActionResult<BasketDto>> CreateOrUpdateBasket(BasketDto basket, CancellationToken ct)
        {
            var result = await _basketService.CreateOrUpdateAsync(basket, ct: ct);
            return ToActionResult(result);
        }
        [HttpDelete("{basketId}")]
        public async Task<ActionResult<BasketDto>> DeleteBasket(string basketId, CancellationToken ct)
        {
            var result = await _basketService.DeleteBasketAsync(basketId, ct);
            return ToActionResult(result);
        }
    }
}
