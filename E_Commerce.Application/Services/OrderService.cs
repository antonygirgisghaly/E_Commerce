using AutoMapper;
using E_Commerce.Application.Comman;
using E_Commerce.Application.Contracts;
using E_Commerce.Application.DTOs.Orders;
using E_Commerce.Application.Specfications;
using E_Commerce.Domain.Contracts;
using E_Commerce.Domain.Entities.Orders;
using E_Commerce.Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application.Services
{
    public class OrderService : IOrderService
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public OrderService(IBasketRepository basketRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _basketRepository = basketRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<Result<OrderToReturnDto>> CreateOrderAsync(OrderDto orderDto, string email, CancellationToken ct = default)
        {
            var basket = await _basketRepository.GetBasketAsync(orderDto.BasketId, ct);
            if(basket == null)
                return Error.NotFound("Basket not found",$"Basket with {orderDto.BasketId} not found");
            if(basket.Items.Count == 0)
                return Error.Validation("Basket is empty", $"Basket with {orderDto.BasketId} is empty");
            //Items (OrderItems)
            var orderItems = new List<OrderItem>(basket.Items.Count);
            var productIds = basket.Items.Select(i => i.Id).ToHashSet();
            var products = (await _unitOfWork.GetGenericRepository<Product,int>()
                .GetAllAsync(new ProductWithSpecfications(productIds),ct)).ToDictionary(p => p.Id);
            foreach (var item in basket.Items)
            {
                if(!products.TryGetValue(item.Id, out var product))
                    return Error.NotFound("Product not found", $"Product with {item.Id} not found");
                orderItems.Add(new OrderItem
                {
                    Price = product.Price,
                    Quantity = item.Quantity,
                    Product = new ProductItemOrder
                    {
                        ProductId = product.Id,
                        ProductName = product.Name,
                        PictureUrl = product.PictureUrl
                    }
                });
            }
            //Shipping Address
            var shippingAddress = _mapper.Map<OrderAddress>(orderDto.ShipToAddress);
            //Delivery Method
            var deliveryMethod = await _unitOfWork.GetGenericRepository<DeliveryMethod, int>()
                .GetByIdAsync(orderDto.DeliveryMethodId, ct);
            if(deliveryMethod == null)
                return Error.NotFound("Delivery method not found", $"Delivery method with {orderDto.DeliveryMethodId} not found");
            //Subtotal
            var subTotal = orderItems.Sum(i => i.Price * i.Quantity);

            // Create Order
            var order = new Order(email, shippingAddress, orderItems, deliveryMethod, subTotal);

            _unitOfWork.GetGenericRepository<Order, Guid>().Add(order); // Local
            var result = await _unitOfWork.SaveChangesAsync(ct);
            if (result == 0)
                return Error.Failure("Order creation failed", "Failed to create order");
            else
            {
                await _basketRepository.DeleteBasketAsync(basket.Id, ct); // Delete basket after order creation
                return _mapper.Map<OrderToReturnDto>(order);
            }
        }
    }
}
