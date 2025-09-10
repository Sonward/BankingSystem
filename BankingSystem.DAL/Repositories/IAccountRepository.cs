using BankingSystem.DAL.Entities;

namespace BankingSystem.DAL.Repositories;

public interface IAccountRepository
{
    public Task<ICollection<Account>> GetAllAsync();
    public Task<Account> GetByIdAsync(Guid id);
    public Task<Account> GetByAccountNumberAsync(string accountNumber);
    public Task<Account> CreateAsync(Account account);
    public Task<Account> UpdateAsync(Account account);
    public Task<bool> DeleteAsync(int id);
}
