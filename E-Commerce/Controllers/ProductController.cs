using E_Commerce.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        [HttpGet("{id}")]
        public ActionResult<Product> GetById(int id)
        {
            return new Product()
            {
                Id = id,
                Name = "ForTest"
            };
        }

        [HttpGet]
        public ActionResult<IEnumerable<Product>> GetAll()
        {
            return new List<Product>();
        }
        [HttpPost]
        public ActionResult<Product> AddProduct(Product product)
        {
            return product;
        }

        [HttpPut]
        public ActionResult<Product> UpdateProduct(Product product)
        {
            return product;
        }

        [HttpDelete]
        public ActionResult<Product> DeleteProduct(Product product)
        {
            return product;
        }
    }
}
