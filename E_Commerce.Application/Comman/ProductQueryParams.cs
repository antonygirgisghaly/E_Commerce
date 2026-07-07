using E_Commerce.Application.Comman.enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application.Comman
{
    public class ProductQueryParams
    {
        public int? TypeId { get; set; }
        public int? BrandId { get; set; }
        public string? SearchValue { get; set; }
        public ProductSortingOptions? Sort { get; set; }
        public int PageIndex { get; set; } = 1;
        private const int DefaultPageSize = 5;
        private const int MaxPageSize = 10;
        private int _pageSize = DefaultPageSize;
        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = (value > MaxPageSize) ? MaxPageSize : (value < 1 ? DefaultPageSize : value);
        }

    }
}
