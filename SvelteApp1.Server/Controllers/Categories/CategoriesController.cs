using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SvelteApp1.Server.Data;
using SvelteApp1.Server.Models;
using SvelteApp1.Server.Services.Categories;

namespace SvelteApp1.Server.Controllers.Categories;


[ApiController]
[Route("[controller]")]
public class CategoriesController : ControllerBase
{
    private readonly ICategoriesService _categoriesService;

    public CategoriesController(ICategoriesService categoriesService)
    {
        _categoriesService = categoriesService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Category>>> GetCategories()
    {
        return Ok(await _categoriesService.GetCategoriesAsync());
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Category>> GetCategory(int id)
    {
        var category = await _categoriesService.GetCategoryAsync(id);
        return category is not null ? Ok(category) : NotFound();
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> PutCategory(int id, Category category)
    {
        if (id != category.Id) return BadRequest();

        try
        {
            await _categoriesService.UpdateCategoryAsync(category);
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
        return NoContent();
    }

    [HttpPost]
    public async Task<ActionResult<Category>> PostCategory(Category category)
    {
        var createdCategory = await _categoriesService.AddCategoryAsync(category);
        return CreatedAtAction(nameof(GetCategory), new { id = createdCategory.Id }, createdCategory);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteCategory(int id)
    {
        try
        {
            await _categoriesService.DeleteCategoryAsync(id);
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
        return NoContent();
    }
}