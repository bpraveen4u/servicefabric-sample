using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ikea.Models;

namespace ProductService
{
    public interface IProductRepository
    {
        Task AddProduct(Product product);
        Task<ICollection<Product>> GetAllProducts();
    }
}
