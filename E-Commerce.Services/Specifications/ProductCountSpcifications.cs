using E_Commerce.Domain.Entities.ProductModule;
using E_Commerce.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Services.Specifications
{
    internal class ProductCountSpcifications : BaseSpecifications<Product, int>
    {
        public ProductCountSpcifications(ProductQueryParams queryParams) : base(ProductSpcificationsHelper.GetProductCriteria(queryParams))
        {

        }
    }
}
