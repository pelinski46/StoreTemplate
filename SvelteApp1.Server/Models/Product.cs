using System.ComponentModel.DataAnnotations;

namespace SvelteApp1.Server.Models;

public class Product
{
    [Key]
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    public int CategoryId { get; set; }
    public virtual Category? Category { get; set; }
    public string? Image {  get; set; }

}
