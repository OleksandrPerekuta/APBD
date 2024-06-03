using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using WebApplication2.Service;

namespace WebApplication2.Controller;

[ApiController]
[Route("api/products")]
public class ProductController : ControllerBase
{
    private readonly IProductService _productService;
        
    public ProductController(IProductService productService)
    {
        _productService = productService;
    }
     
    [HttpPost]
    public async Task<IActionResult> CreateProduct(ProductDtoRequest productDtoRequest, IValidator<ProductDtoRequest> validator)
    {
        var validationResult = await validator.ValidateAsync(productDtoRequest);
        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors);
        }
        
        var product = await _productService.CreateProduct(productDtoRequest);
        return Ok(product);
    }
}