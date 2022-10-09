using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyWebApiApp.Models;

namespace MyWebApiApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        public static List<Product> products = new List<Product>();

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(products);
        }

        [HttpPost]
        public IActionResult Create(ProductVM productVM)
        {
            var product = new Product
            {
                Code = Guid.NewGuid().ToString(),
                Name = productVM.Name,
                Price = productVM.Price
            };

            products.Add(product);

            return Ok(new
            {
                Success = true,
                Data = product
            });
        }

        [HttpGet("{id}")]
        public IActionResult GetById( string id)
        {
            try
            {
                // LINQ [Object] Query
                var product = products.SingleOrDefault(p => p.Code == Guid.Parse(id).ToString());
                if (product == null)
                {
                    return NotFound();
                }
                return Ok(product);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPut("{id}")]
        public IActionResult Edit(string id, Product productEdit)
        {
            try
            {
                // LINQ [Object] Query
                var product = products.SingleOrDefault(p => p.Code == Guid.Parse(id).ToString());
                if (product == null)
                {
                    return NotFound();
                }

                if (id != product.Code.ToString())
                {
                    return BadRequest();
                }
                // Update
                product.Name = productEdit.Name;
                product.Price = productEdit.Price;

                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Remove(string id)
        {
            try
            {
                // LINQ [Object] Query
                var product = products.SingleOrDefault(p => p.Code == Guid.Parse(id).ToString());
                if (product == null)
                {
                    return NotFound();
                }

               
                // Delete
                products.Remove(product);

                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
