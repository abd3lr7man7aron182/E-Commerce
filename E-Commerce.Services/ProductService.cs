using AutoMapper;
using E_Commerce.Domain.Contract;
using E_Commerce.Domain.Entities.ProductModule;
using E_Commerce.Services.Specifications;
using E_Commerce.Services_Abstraction;
using E_Commerce.Shared;
using E_Commerce.Shared.DTOs.ProductDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Services
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public ProductService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        public async Task<IEnumerable<BrandDTO>> GetAllBrandsAsync()
        {
            var Brands = await unitOfWork.GetRepository<ProductBrand, int>().GetAllAsync();
            return mapper.Map<IEnumerable<BrandDTO>>(Brands);
        }

        public async Task<PaginatedResult<ProductDTO>> GetAllProductsAsync(ProductQueryParams queryParams)
        {
            //spcifi -> get all prods including[prodtype,prodbrnad]
            //filter with brandId,TypeId if needed
            var Repo = unitOfWork.GetRepository<Product, int>();
            var spec = new ProductWithTypeAndBrandspecification(queryParams);
            var Products = await Repo.GetAllAsync(spec);
            var DataToReturn = mapper.Map<IEnumerable<ProductDTO>>(Products);
            var CountOfReturnedData = DataToReturn.Count();
            var CountSpec = new ProductCountSpcifications(queryParams);
            var COuntOfAllProducts = await Repo.CountAsync(CountSpec);
            return new PaginatedResult<ProductDTO>(queryParams.PageIndex, CountOfReturnedData, COuntOfAllProducts, DataToReturn);
        }

        public async Task<IEnumerable<TypeDTO>> GetAllTypesAsync()
        {
            var Types = await unitOfWork.GetRepository<ProductType, int>().GetAllAsync();
            return mapper.Map<IEnumerable<TypeDTO>>(Types);
        }

        public async Task<ProductDTO> GetProductByIdAsync(int id)
        {
            //spcifi -> get prod by id including[prodtype,prodbrnad]
            var spec = new ProductWithTypeAndBrandspecification(id);
            var Product = await unitOfWork.GetRepository<Product, int>().GetByIdAsync(spec);
            return mapper.Map<ProductDTO>(Product);
        }
    }
}
