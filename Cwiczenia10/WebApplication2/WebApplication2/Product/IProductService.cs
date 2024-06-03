using WebApplication2.Controller;

namespace WebApplication2.Service;

public interface IProductService
{
    Task<ProductDtoResponse> CreateProduct(ProductDtoRequest productDtoRequest);
}