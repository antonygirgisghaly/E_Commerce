using E_Commerce.Domain.Entities.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application.Specfications
{
    internal class OrderSpecfication : BaseSpecfication<Order,Guid>
    {
        public OrderSpecfication(string email):base(b => b.BuyerEmail == email)
        {
            AddInclude(x => x.DeliveryMethod);
            AddInclude(x => x.Items);
            AddOrderByDescending(x => x.OrderDate);
        }
        public OrderSpecfication(string email,Guid id) : base(a => a.Id == id && a.BuyerEmail == email)
        {
            AddInclude(x => x.DeliveryMethod);
            AddInclude(x => x.Items);
        }
    }
}
