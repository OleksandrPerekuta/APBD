using Microsoft.EntityFrameworkCore;
using WebApplication2.Controller;
using WebApplication2.Entities;
using WebApplication2.Exception;

namespace WebApplication2.Service;

public class ProductService : IProductService
{
    private readonly Context.Context _context;

    public ProductService(Context.Context context)
    {
        _context = context;
    }

    public async Task<ProductDtoResponse> CreateProduct(ProductDtoRequest productDtoRequest)
    {
        var product = new Product
        {
            Name = productDtoRequest.Name,
            Width = productDtoRequest.Width,
            Height = productDtoRequest.Height,
            Depth = productDtoRequest.Depth,
            Weight = productDtoRequest.Weight
        };

        var categories = await _context.Category
            .Where(e => productDtoRequest.Categories.Contains(e.CategoryId))
            .ToListAsync();

        if (categories.Count != productDtoRequest.Categories.Count)
        {
            throw new EntityNotFoundException("Some categories do not exist");
        }

        foreach (var category in categories)
        {
            product.Categories.Add(category);
        }

        await _context.Products.AddAsync(product);
        await _context.SaveChangesAsync();

        return new ProductDtoResponse
        {
            ProductId = product.ProductId,
            Name = product.Name,
            Width = product.Width,
            Height = product.Height,
            Depth = product.Depth,
            Weight = product.Weight,
            Categories = CreateCategoriesResponse(product.Categories)
        };
    }

    private List<object> CreateCategoriesResponse(ICollection<Category> categories)
    {
        return categories.Select(category => new
        {
            category.CategoryId,
            category.Name
        }).ToList<object>();
    }
}