using Bogus;
using Microsoft.EntityFrameworkCore;
using SvelteApp1.Server.Data;
using SvelteApp1.Server.Models;

namespace SvelteApp1.Server.Services.Products;

public class ProductService : IProductService
{
    private readonly ApplicationDbContext _context;

    public ProductService(ApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<List<Product>> GetProductsAsync()
    {
        return await _context.Products.ToListAsync();
    }

    public async Task<List<Product>> GetProductsByCategoryAsync(int categoryId)
    {
        return await _context.Products.Where(p => p.CategoryId == categoryId).ToListAsync();
    }

    public async Task<Product> GetProductByIdAsync(int id)
    {
        return await _context.Products.FindAsync(id) ?? throw new InvalidOperationException();
    }

    public async Task<bool> UpdateProductAsync(Product product)
    {
        _context.Entry(product).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
            return true;
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!await ProductExistsAsync(product.Id))
            {
                return false;
            }
            else
            {
                throw;
            }
        }
    }

    public async Task<Product> CreateProductAsync(Product product)
    {
        _context.Products.Add(product);
        await _context.SaveChangesAsync();
        return product;
    }

    public async Task<bool> DeleteProductAsync(int id)
    {
        var product = await _context.Products.FindAsync(id);
        if (product == null)
        {
            return false;
        }

        _context.Products.Remove(product);
        await _context.SaveChangesAsync();
        return true;
    }

    private async Task<bool> ProductExistsAsync(int id)
    {
        return await _context.Products.AnyAsync(e => e.Id == id);
    }

    public async Task GenerateFakeDataAsync(int count = 50)
    {
        var faker = new Faker("es");

        for (var i = 1; i <= count; i++)
        {
            var product = new Product
            {
                Id = i,
                Title = faker.Commerce.ProductName(),
                Description = faker.Commerce.ProductDescription(),
                Quantity = faker.Random.Number(1, 100),
                Price = decimal.Parse(faker.Commerce.Price()),
                CategoryId = faker.Random.Number(1, 5),
                Image = faker.Image.PicsumUrl()
            };

            await CreateProductAsync(product);
        }
    }
}