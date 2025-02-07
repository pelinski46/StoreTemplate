using SvelteApp1.Server.Models;

namespace SvelteApp1.Server.Services.Products;

public interface IProductService
{
    Task<List<Product>> GetProductsAsync();
    Task<List<Product>> GetProductsByCategoryAsync(int categoryId);
    Task<Product> GetProductByIdAsync(int id);
    Task<bool> UpdateProductAsync(Product product);
    Task<Product> CreateProductAsync(Product product);
    Task<bool> DeleteProductAsync(int id);
    Task GenerateFakeDataAsync(int count = 50);
}