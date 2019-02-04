using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.ServiceFabric.Data;
using Microsoft.ServiceFabric.Data.Collections;
using System.Threading;
using Ikea.Models;

namespace ProductService
{
    public class ProductRepository : IProductRepository
    {
        private readonly IReliableStateManager _stateManager;

        public ProductRepository(IReliableStateManager stateManager)
        {
            this._stateManager = stateManager;
        }

        public async Task AddProduct(Product product)
        {
            var products = await _stateManager.GetOrAddAsync<IReliableDictionary<Guid, Product>>("ProductsList");
            using (var tx = _stateManager.CreateTransaction())
            {
                await products.AddOrUpdateAsync(tx, product.ID, product, (key, value) => product);
                // what is this updateValueFactory, last param)
                await tx.CommitAsync();
            }
            //throw new NotImplementedException();
        }

        public async Task<ICollection<Product>> GetAllProducts()
        {
            var products = await _stateManager.GetOrAddAsync<IReliableDictionary<Guid, Product>>("ProductsList");
            var result = new List<Product>();
            using (var tx = _stateManager.CreateTransaction())
            {
                var myEnumerable = await products.CreateEnumerableAsync(tx);
                // Adding this enumerable to collection ? cant .net provide in-built function to convert
                using (var myEnumerator = myEnumerable.GetAsyncEnumerator())
                {
                    while (await myEnumerator.MoveNextAsync(CancellationToken.None))
                    {
                        KeyValuePair<Guid, Product> tempObject = myEnumerator.Current;
                        result.Add(tempObject.Value);
                    }
                }
                return result;
            }

            //throw new NotImplementedException();
        }
    }
}
