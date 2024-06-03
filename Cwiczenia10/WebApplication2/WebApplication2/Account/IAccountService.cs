using WebApplication2.Controller;

namespace WebApplication2.Service;

public interface IAccountService
{
    Task<AccountDtoResponse> GetAccountById(int id); 
}