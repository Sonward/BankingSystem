using BankingSystem.BLL.Services;
using BankingSystem.DTO;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BankingSystem.Controllers
{
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
        public async Task<ActionResult<AccountDTO>> CreateAsync(AccountCreateRequest request)
        {
            var account = await accountService.CreateAccountAsync(request);
            return Ok(account);
        }
    }
}
