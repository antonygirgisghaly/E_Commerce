using E_Commerce.Application.Comman;
using E_Commerce.Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application.Specfications
{
    internal class ProductCountSpecfication : BaseSpecfication<Product,int>
    {
        public ProductCountSpecfication(ProductQueryParams queryParams) :
             base(p => (!queryParams.BrandId.HasValue || p.BrandId == queryParams.BrandId.Value) &&
            (!queryParams.TypeId.HasValue || p.TypeId == queryParams.TypeId.Value) &&
            (string.IsNullOrWhiteSpace(queryParams.SearchValue) || p.Name.ToLower().Contains(queryParams.SearchValue.ToLower())))
        {
            
        }
    }
}
