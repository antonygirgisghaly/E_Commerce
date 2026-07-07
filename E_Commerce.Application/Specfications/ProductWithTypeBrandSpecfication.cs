using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_Commerce.Domain.Entities.Products;
using E_Commerce.Application.Comman;

namespace E_Commerce.Application.Specfications
{
    internal class ProductWithTypeBrandSpecfication : BaseSpecfication<Product, int>
    {
        public ProductWithTypeBrandSpecfication(ProductQueryParams queryParams, CancellationToken ct) : 
            base(p => (!queryParams.BrandId.HasValue || p.BrandId == queryParams.BrandId.Value) && 
            (!queryParams.TypeId.HasValue || p.TypeId == queryParams.TypeId.Value) &&
            (string.IsNullOrWhiteSpace(queryParams.SearchValue) || p.Name.ToLower().Contains(queryParams.SearchValue.ToLower())))
        {
            AddInclude(p => p.ProductType);
            AddInclude(p => p.ProductBrand);

            switch (queryParams.Sort)
            {
                case Comman.enums.ProductSortingOptions.NameAsc:
                    AddOrderBy(p => p.Name);
                    break;
                case Comman.enums.ProductSortingOptions.NameDesc:
                    AddOrderByDescending(p => p.Name);
                    break;
                case Comman.enums.ProductSortingOptions.PriceAsc:
                    AddOrderBy(p => p.Price);
                    break;
                case Comman.enums.ProductSortingOptions.PriceDesc:
                    AddOrderByDescending(p => p.Price);
                    break;
                default:
                    AddOrderBy(p => p.Id);
                    break;
            }
                ApplyPagination(queryParams.PageSize, queryParams.PageIndex);
        }
        public ProductWithTypeBrandSpecfication(int id) : base(x => x.Id == id)
        {
            AddInclude(p => p.ProductType);
            AddInclude(p => p.ProductBrand);
        }
    }
}
