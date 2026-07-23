using E_Commerce.Application.Services;
using E_Commerce.Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application.Specfications
{
    internal class ProductWithSpecfications : BaseSpecfication<Product,int>
    {
        public ProductWithSpecfications(HashSet<int> productIds) : base(p => productIds.Contains(p.Id))
        {
            
        }
    }
}
