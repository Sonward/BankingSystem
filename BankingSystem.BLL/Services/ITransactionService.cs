using BankingSystem.DTO.EntityDTO;

namespace BankingSystem.BLL.Services;

public interface ITransactionService
{
    public Task<TransactionDTO> GetTransactionById(Guid id);
    public Task<ICollection<TransactionDTO>> GetByAccountNumber(string accountNumber);
    public Task<TransactionDTO> DepositAsync(string targetNumber, decimal amount);
    public Task<TransactionDTO> WithdrawAsync(string targetNumber, decimal amount);
    public Task<TransferTransactionDTO> TransferAsync(string targetFromNumber, string targetToNumber, decimal amount);
}
