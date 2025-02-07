using Microsoft.EntityFrameworkCore;
using SvelteApp1.Server.Data;
using SvelteApp1.Server.Models;

namespace SvelteApp1.Server.Services.Categories;

public class CategoriesService : ICategoriesService
{

    private readonly ApplicationDbContext _context;
    public CategoriesService(ApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<Category> AddCategoryAsync(Category category)
    {
        await _context.Categories.AddAsync(category);
        await _context.SaveChangesAsync();
        return category;
    }

    public async Task<bool> CategoryExistsAsync(int id)
    {
        return await _context.Categories.AnyAsync(c => c.Id == id);
    }

    public async Task DeleteCategoryAsync(int id)
    {
        var category = await GetCategoryAsync(id)
            ?? throw new KeyNotFoundException($"Category with ID {id} not found");
        _context.Categories.Remove(category);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<Category>> GetCategoriesAsync()
    {
        return await _context.Categories.ToListAsync();
    }

    public async Task<Category?> GetCategoryAsync(int id)
    {
        return await _context.Categories.FindAsync(id);
    }

    public async Task UpdateCategoryAsync(Category category)
    {
        _context.Entry(category).State = EntityState.Modified;
        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!await CategoryExistsAsync(category.Id))
            {
                throw new KeyNotFoundException($"Category with ID {category.Id} not found");
            }
            throw;
        }
    }
}
