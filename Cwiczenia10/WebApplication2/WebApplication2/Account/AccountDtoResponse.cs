namespace WebApplication2.Controller;

public class AccountDtoResponse
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string? Phone { get; set; }
    public string Role { get; set; }
    public ICollection<object> Carts { get; set; }
}