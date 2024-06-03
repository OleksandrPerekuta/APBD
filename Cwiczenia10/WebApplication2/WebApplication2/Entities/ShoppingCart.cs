namespace WebApplication2.Entities;

public class ShoppingCart
{
    public Account Account { get; set; }
    public Product Product { get; set; }
    public int Amount { get; set; }
}