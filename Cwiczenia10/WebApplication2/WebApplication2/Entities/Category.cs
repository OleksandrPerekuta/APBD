namespace WebApplication2.Entities;

using System.ComponentModel.DataAnnotations;

public class Category
{
    [Key]
    public int CategoryId { get; set; }

    [MaxLength(100)]
    [Required]
    public string Name { get; set; }

    public ICollection<Product> Products { get; } = new List<Product>();
}