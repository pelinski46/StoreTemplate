using Bogus;
using Microsoft.EntityFrameworkCore;
using SvelteApp1.Server.Data;
using SvelteApp1.Server.Models;

namespace SvelteApp1.Server.Services;

public class ProductService(ApplicationDbContext context)
{
    public async Task<List<Product>> GetProductsAsync()
    {
        return await context.Products.ToListAsync();
    }

    public async Task<List<Product>> GetProductsByCategoryAsync(int categoryId)
    {
        return await context.Products.Where(p => p.CategoryId == categoryId).ToListAsync();
    }

    public async Task<Product> GetProductByIdAsync(int id)
    {
        return await context.Products.FindAsync(id) ?? throw new InvalidOperationException();
    }

    public async Task<bool> UpdateProductAsync(Product product)
    {
        context.Entry(product).State = EntityState.Modified;

        try
        {
            await context.SaveChangesAsync();
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
        context.Products.Add(product);
        await context.SaveChangesAsync();
        return product;
    }

    public async Task<bool> DeleteProductAsync(int id)
    {
        var product = await context.Products.FindAsync(id);
        if (product == null)
        {
            return false;
        }

        context.Products.Remove(product);
        await context.SaveChangesAsync();
        return true;
    }

    private async Task<bool> ProductExistsAsync(int id)
    {
        return await context.Products.AnyAsync(e => e.Id == id);
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