using E_Commerce.Application.Comman;
using E_Commerce.Application.DTOs.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application.Contracts
{
    public interface IOrderService
    {
        Task<Result<OrderToReturnDto>> CreateOrderAsync(OrderDto orderDto, string email,CancellationToken ct = default);
        Task<Result<IReadOnlyList<OrderToReturnDto>>> GetAllOrdersAsync(string email,CancellationToken ct = default);
        Task<Result<OrderToReturnDto>> GetOrderByIdandEmailAsync(Guid id,string email,CancellationToken ct = default);
    }
}
