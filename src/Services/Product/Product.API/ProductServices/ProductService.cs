using Confluent.Kafka;
using Product.API.Dto;
using SharedLibrary;
using System.Text.Json;

namespace Product.API.ProductServices
{
    public class ProductService : IProductService
    {
        private readonly IProducer<Null, string> _producer;
        private readonly List<Products> _products = new List<Products>();

        public ProductService(IProducer<Null, string> producer)
        {
            _producer = producer;
        }
        public async Task<Guid> AddProduct(ProductRequest products)
        {
            if (products == null)
            {
                throw new ArgumentNullException(nameof(Products));
            }

            var p = new Products()
            {
                Id = Guid.NewGuid(),
                Name = products.Name,
                ProductCode = products.ProductCode,
                Price = products.Price,
                Description = products.Description,
            };

            _products.Add(p);

            var result = await _producer.ProduceAsync("add-product", new Message<Null, string> { Value = JsonSerializer.Serialize(p) });

            if (result.Status != PersistenceStatus.Persisted)
            {
                var lastData = _products.Last();
                if (lastData != null)
                    _products.Remove(lastData);
                throw new InvalidOperationException("Failed to persist product data in Kafka.");
            }

            return p.Id;
        }

        public async Task<bool> DeleteProduct(Guid id)
        {
            var product = _products.FirstOrDefault(x => x.Id == id);
            if (product != null)
            {
                _products.Remove(product);

                var result = await _producer.ProduceAsync("delete-product", new Message<Null, string> { Value = product.Id.ToString() });

                if (result.Status != PersistenceStatus.Persisted)
                {
                    var lastData = _products.Last();
                    if (lastData != null)
                        _products.Remove(lastData);
                    throw new InvalidOperationException("Failed to persist product data in Kafka.");
                }

                return await Task.FromResult(true);
            }

            return await Task.FromResult(false);
        }
    }
}
