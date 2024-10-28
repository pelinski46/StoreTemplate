namespace SvelteApp1.Server.Models;

public class Category
{
    public int Id { get; set; }
    public string Name { get; set; }
    public ICollection<Product> Products { get; } = new List<Product>();

}