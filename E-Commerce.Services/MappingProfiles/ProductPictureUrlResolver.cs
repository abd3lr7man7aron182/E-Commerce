using AutoMapper;
using E_Commerce.Domain.Entities.ProductModule;
using E_Commerce.Shared.DTOs.ProductDTOs;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace E_Commerce.Services.MappingProfiles
{
    public class ProductPictureUrlResolver : IValueResolver<Product, ProductDTO, string>
    {
        private readonly IConfiguration configuration;

        public ProductPictureUrlResolver(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public string Resolve(Product source, ProductDTO destination, string destMember, ResolutionContext context)
        {
            if(string.IsNullOrEmpty(source.PictureUrl)) return string.Empty;
            
            if (source.PictureUrl.StartsWith("http")) return source.PictureUrl;

            //var picURL = $"{https://localhost:7248}/{source.PictureUrl}";
            var BaseURL = configuration.GetSection("URLs")["BaseUrl"];
            var picURL = $"{BaseURL}{source.PictureUrl}";
            return picURL;
        }
    }
}
