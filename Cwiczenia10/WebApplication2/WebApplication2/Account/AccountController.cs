using Microsoft.AspNetCore.Mvc;
using WebApplication2.Service;

namespace WebApplication2.Controller;

[ApiController]
[Route("api/accounts")]
public class AccountController : ControllerBase
{
    private readonly IAccountService _accountService;
        
    public AccountController(IAccountService accountService)
    {
        _accountService = accountService;
    }
        
    [HttpGet("{id}")]
    public async Task<IActionResult> GetAccount(int id)
    {
        var account = await _accountService.GetAccountById(id);
        if (account == null)
        {
            return NotFound();
        }
        return Ok(account);
    }
}