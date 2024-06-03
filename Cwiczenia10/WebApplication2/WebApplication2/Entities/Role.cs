namespace WebApplication2.Entities;
using System.ComponentModel.DataAnnotations;

public class Role
{
    [Key]
    public int RoleId { get; set; }

    [MaxLength(100)]
    [Required]
    public string Name { get; set; }

    public ICollection<Account> Accounts { get; } = new List<Account>();
}