using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ikea.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.ServiceFabric.Services.Client;
using Microsoft.ServiceFabric.Services.Remoting.Client;

namespace ProductManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private IIkeaProductService ikeaProductService;
        public ProductsController()
        {
            ikeaProductService = ServiceProxy.Create<IIkeaProductService>(new Uri("fabric:/IkeaCart1/ProductService"), new ServicePartitionKey(0));
        }
        // GET api/values
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> Get()
        {
            var products = await ikeaProductService.GetAllProducts();
            return Ok(products);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public async Task Post([FromBody] Product value)
        {
            await ikeaProductService.AddProduct(value);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
