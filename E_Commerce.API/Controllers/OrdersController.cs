using E_Commerce.Application.Contracts;
using E_Commerce.Application.DTOs.Orders;
using E_Commerce.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.API.Controllers
{
    public class OrdersController : ApiBaseController
    {
        private readonly IOrderService _orderService;
        public OrdersController(IOrderService service)
        {
            _orderService = service;
        }
        //Post Create Orders
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<OrderToReturnDto>> CreateOrder(OrderDto orderDto,CancellationToken ct =default)
        {
            var result = await _orderService.CreateOrderAsync(orderDto, GetEmailFromToken(), ct);
            return ToActionResult(result); 
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<OrderToReturnDto>>> GetAllOrders(CancellationToken ct =default)
        {
            return ToActionResult(await _orderService.GetAllOrdersAsync(GetEmailFromToken(), ct));
        }
        [Authorize]
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<OrderToReturnDto>> GetOrderById(Guid id,CancellationToken ct = default)
        {
            return ToActionResult(await _orderService.GetOrderByIdandEmailAsync(id,GetEmailFromToken(),ct));
        } 
    }
}
