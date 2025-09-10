using BankingSystem.BLL.Services;
using BankingSystem.DTO.EntityDTO;
using BankingSystem.DTO.Requests;
using Microsoft.AspNetCore.Mvc;

namespace BankingSystem.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AccountController(IAccountService accountService) : ControllerBase
{
    [HttpGet("all")]
    [ProducesResponseType(200, Type = typeof(IEnumerable<AccountDTO>))]
    public async Task<ActionResult<IEnumerable<AccountDTO>>> GetAllAsync()
    {
        var accounts = await accountService.GetAllAccountsAsync();
        return Ok(accounts);
    }

    [HttpGet("accountNumber")]
    [ProducesResponseType(200, Type = typeof(AccountDTO))]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    public async Task<ActionResult<AccountDTO>> GetByAccountNumberAsync(string accountNumber)
    {
        if (string.IsNullOrWhiteSpace(accountNumber))
            return BadRequest("accountNumber is required.");

        var account = await accountService.GetAccountByNumberAsync(accountNumber);
        if (account == null)
            return NotFound();

        return Ok(account);
    }

    [HttpPost("create")]
    [ProducesResponseType(200, Type = typeof(AccountDTO))]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<AccountDTO>> CreateAsync(AccountCreateRequest request)
    {
        try
        {
            var account = await accountService.CreateAccountAsync(request);
            return Ok(account);
        }
        catch (ArgumentNullException ex)
        {
            return BadRequest($"Обов'язковий параметр відсутній: {ex.ParamName}");
        }
        catch (ArgumentException ex)
        {
            return BadRequest($"Невірний параметр: {ex.Message}");
        }
        catch
        {
            return StatusCode(500, "Внутрішня помилка сервера");
        }
    }
}
