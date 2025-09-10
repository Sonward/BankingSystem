using BankingSystem.DAL.Entities;

namespace BankingSystem.DAL.Repositories;

public interface ITransactionRepository
{
    public Task<Transaction> CreateAsync(Transaction transaction);
    public Task<Transaction> GetByIdAsync(Guid id);
    public Task<ICollection<Transaction>> GetByAccountNumberAsync(string accountNumber);
}
