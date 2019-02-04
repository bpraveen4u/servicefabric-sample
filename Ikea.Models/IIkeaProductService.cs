using Microsoft.ServiceFabric.Services.Remoting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ikea.Models
{
    public interface IIkeaProductService: IService
    {
        Task AddProduct(Product product);
        Task<IEnumerable<Product>> GetAllProducts();
    }
}
