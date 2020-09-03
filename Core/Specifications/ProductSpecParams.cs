using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Specifications
{
    public class ProductSpecParams
    {
        const int MaxPageSize = 50;
        public int PageIndex { get; set; } = 1;
        int _pageSize = MaxPageSize;
        public int PageSize 
        {
            get => _pageSize;
            set => _pageSize = Math.Min(value, MaxPageSize);
        }
        public string Sort { get; set; }
        public int? ProductCategoryId { get; set; }
        public int? ProductModelId { get; set; }
    }
}
