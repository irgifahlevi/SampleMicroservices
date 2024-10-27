using Confluent.Kafka;
using Product.API.Dto;
using SharedLibrary;
using System.Runtime.CompilerServices;
using System.Text.Json;

namespace Product.API.ProductServices
{
    public interface IProductService
    {
        Task<Guid> AddProduct(ProductRequest products);
        Task<bool> DeleteProduct(Guid id);
    }

}
