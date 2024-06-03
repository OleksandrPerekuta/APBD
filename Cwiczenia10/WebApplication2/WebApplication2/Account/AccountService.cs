using Microsoft.EntityFrameworkCore;
using WebApplication2.Controller;
using WebApplication2.Entities;
using WebApplication2.Exception;

namespace WebApplication2.Service;

public class AccountService : IAccountService
{
    private readonly Context.Context _context;

    public AccountService(Context.Context context)
    {
        _context = context;
    }

    public async Task<AccountDtoResponse> GetAccountById(int id) 
    {
        var account = await _context.Accounts
            .Include(e => e.ShoppingCarts)
            .ThenInclude(e1 => e1.Product)
            .Include(e => e.Role)
            .FirstOrDefaultAsync(e => e.AccountId == id);

        if (account == null)
        {
            throw new EntityNotFoundException($"Account with id {id} not found");
        }

        var shoppingCarts = CreateShoppingList(account.ShoppingCarts);

        return new AccountDtoResponse
        {
            FirstName = account.FirstName,
            LastName = account.LastName,
            Email = account.Email,
            Phone = account.Phone,
            Role = account.Role.Name,
            Carts = shoppingCarts
        };
    }

    private List<object> CreateShoppingList(ICollection<ShoppingCart> shoppingCarts)
    {
        var shoppingList = new List<object>();

        foreach (var cart in shoppingCarts)
        {
            shoppingList.Add(new
            {
                cart.Product.ProductId,
                cart.Product.Name,
                cart.Amount
            });
        }

        return shoppingList;
    }
}