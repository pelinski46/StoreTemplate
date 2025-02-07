using SvelteApp1.Server.Models;

namespace SvelteApp1.Server.Services.Categories;

public interface ICategoriesService
{
    Task<IEnumerable<Category>> GetCategoriesAsync();
    Task<Category?> GetCategoryAsync(int id);
    Task<Category> AddCategoryAsync(Category category);
    Task UpdateCategoryAsync(Category category);
    Task DeleteCategoryAsync(int id);
    Task<bool> CategoryExistsAsync(int id);
}
