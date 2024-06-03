namespace WebApplication2.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
public class Product
{
    [Key]
    public int ProductId { get; set; }

    public ICollection<Category> Categories { get; } = new List<Category>();

    public ICollection<ShoppingCart> ShoppingCarts { get; } = new List<ShoppingCart>();

    public ICollection<Account> Accounts { get; } = new List<Account>();

    [MaxLength(100)]
    [Required]
    public string Name { get; set; }

    [Column(TypeName = "decimal(5, 2)")]
    public decimal Weight { get; set; }

    [Column(TypeName = "decimal(5, 2)")]
    public decimal Width { get; set; }

    [Column(TypeName = "decimal(5, 2)")]
    public decimal Height { get; set; }

    [Column(TypeName = "decimal(5, 2)")]
    public decimal Depth { get; set; }
}