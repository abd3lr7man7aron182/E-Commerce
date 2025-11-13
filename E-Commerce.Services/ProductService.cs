using AutoMapper;
using E_Commerce.Domain.Contract;
using E_Commerce.Domain.Entities.ProductModule;
using E_Commerce.Services_Abstraction;
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

        public ProductService(IUnitOfWork unitOfWork ,IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        public async Task<IEnumerable<BrandDTO>> GetAllBrandsAsync()
        {
            var Brands=await unitOfWork.GetRepository<ProductBrand,int>().GetAllAsync();
            return mapper.Map<IEnumerable<BrandDTO>>(Brands);
        }
         
        public async Task<IEnumerable<ProductDTO>> GetAllProductsAsync()
        {
           var Products=await unitOfWork.GetRepository<Product,int>().GetAllAsync();
            return mapper.Map<IEnumerable<ProductDTO>>(Products);
        }

        public async Task<IEnumerable<TypeDTO>> GetAllTypesAsync()
        {
           var Types =await unitOfWork.GetRepository<ProductType,int>().GetAllAsync();   
            return mapper.Map<IEnumerable<TypeDTO>>(Types);
        }

        public async Task<ProductDTO> GetProductByIdAsync(int id)
        {
            var Product = await unitOfWork.GetRepository<Product, int>().GetByIdAsync(id);
            return mapper.Map<ProductDTO>(Product);
        }
    }
}
