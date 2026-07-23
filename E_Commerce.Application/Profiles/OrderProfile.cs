using AutoMapper;
using E_Commerce.Application.DTOs.Identity;
using E_Commerce.Application.DTOs.Orders;
using E_Commerce.Domain.Entities.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application.Profiles
{
    internal class OrderProfile : Profile
    {
        public OrderProfile() 
        {
            CreateMap<AddressDto, OrderAddress>().ReverseMap();
            CreateMap<Order, OrderToReturnDto>()
                .ForMember(o => o.DeliveryMethod, opt => opt.MapFrom(s => s.DeliveryMethod.ShortName))
                .ForMember(o => o.DeliveryMethodCost, opt => opt.MapFrom(s => s.DeliveryMethod.Cost));
            CreateMap<OrderItem, OrderItemDto>()
                .ForMember(o => o.ProductId, opt => opt.MapFrom(s => s.Product.ProductId))
                .ForMember(o => o.ProductName, opt => opt.MapFrom(s => s.Product.ProductName))
                .ForMember(o => o.PictureUrl, opt => opt.MapFrom<OrderItemPictureUrlResolver>());
            CreateMap<DeliveryMethod, DeleviryMethodDto>();
        }
    }
}
