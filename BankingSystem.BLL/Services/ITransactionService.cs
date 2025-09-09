using BankingSystem.DTO.EntityDTO;

namespace BankingSystem.BLL.Services
{
    public interface ITransactionService
    {
        public Task<TransactionDTO> GetTransactionById(Guid id);
        public Task<ICollection<TransactionDTO>> GetByAccountNumber(string accountNumber);
        public Task<TransactionDTO> DepositAsync(AccountDTO target, decimal amount);
        public Task<TransactionDTO> WithdrawAsync(AccountDTO target, decimal amount);
        public Task<TransferTransactionDTO> TransferAsync(AccountDTO targetFrom, AccountDTO targetTo, decimal amount);
    }
}
