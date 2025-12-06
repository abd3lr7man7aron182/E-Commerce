using E_Commerce.Domain.Entities.ProductModule;
using E_Commerce.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Services.Specifications
{
    internal class ProductWithTypeAndBrandspecification : BaseSpecifications<Product, int>
    {
        public ProductWithTypeAndBrandspecification(int id) : base(p => p.Id == id)
        {
            AddInclude(p => p.ProductType);
            AddInclude(p => p.ProductBrand);
        }
        //ctor for get all products
        public ProductWithTypeAndBrandspecification(ProductQueryParams queryParams)
            : base(ProductSpcificationsHelper.GetProductCriteria(queryParams))
        //cases:
        //p=>p.BrandId=brandid -> brandId is not null
        //p=>p.TypeId=typeid -> typeid is not null 
        //p=>p.TypeId=typeid  && p=>p.BrandId=brandid -> typeid is not null &&  brandId is not null
        {
            AddInclude(p => p.ProductType);
            AddInclude(p => p.ProductBrand);
            switch (queryParams.Sort)
            {
                case ProductSortingOptions.NameAsc:
                    AddOrderBy(p => p.Name); break;
                case ProductSortingOptions.NameDesc:
                    AddOrderByDescending(p => p.Name); break;
                case ProductSortingOptions.PriceAsc:
                    AddOrderBy(p => p.Price); break;
                case ProductSortingOptions.PriceDesc:
                    AddOrderByDescending(p => p.Price); break;
                default:
                    AddOrderBy(p => p.Id); break;
            }

            ApplyPagination(queryParams.PageSize, queryParams.PageIndex);
        }
    }
}
