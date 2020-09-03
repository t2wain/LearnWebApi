using Core.Entities;
using Core.Specifications;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Specifications
{
    public class ProductSpec : BaseSpecification<Product>
    {
        public ProductSpec(ProductSpecParams productParams) : base()
        {
            this.SetIncludes();
            var s = productParams;

            Criteria = (p => 
                (!s.ProductCategoryId.HasValue || p.ProductCategoryId == s.ProductCategoryId)
                && (!s.ProductModelId.HasValue || p.ProductModelId == s.ProductModelId)
            );

            if (!String.IsNullOrWhiteSpace(s.Sort))
            {
                switch (s.Sort)
                {
                    case "nameAsc":
                        OrderBy = (p => p.Name);
                        break;
                    case "nameDesc":
                        OrderByDescending = (p => p.Name);
                        break;
                }
            }
            ApplyPaging(s.PageSize, s.PageIndex);
        }
        public ProductSpec(int id)
        {
            this.Criteria = (p => p.ProductId == id);
            this.SetIncludes();
        }
        protected void SetIncludes()
        {
            Includes.Add(p => p.ProductCategory);
            Includes.Add(p => p.ProductModel);
        }
    }
}
