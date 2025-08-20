using BankingSystem.BLL.Services;
using BankingSystem.DTO;
using BankingSystem.DTO.TransactionRequests;
using Microsoft.AspNetCore.Mvc;

namespace BankingSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController(ITransactionService transactionService) : ControllerBase
    {
        [HttpPost("deposit")]
        [ProducesResponseType(200, Type = typeof(TransactionDTO))]
        public async Task<ActionResult<TransactionDTO>> Deposit([FromBody] TransactionCreateRequest request)
        {
            var transaction = await transactionService.DepositAsync(request.Target, request.Amount);
            return Ok(transaction);
        }

        [HttpPost("withdraw")]
        [ProducesResponseType(200, Type = typeof(TransactionDTO))]
        public async Task<ActionResult<TransactionDTO>> Withdraw([FromBody] TransactionCreateRequest request)
        {
            var transaction = await transactionService.WithdrawAsync(request.Target, request.Amount);
            return Ok(transaction);
        }

        [HttpPost("transfer")]
        [ProducesResponseType(200, Type = typeof(TransferTransactionDTO))]
        public async Task<ActionResult<TransferTransactionDTO>> Transfer([FromBody] TransferTransactionCreateRequest request)
        {
            var transaction = await transactionService.TransferAsync(request.TargetFrom, request.TargetTo, request.Amount);
            return Ok(transaction);
        }
    }
}
