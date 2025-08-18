using BankingSystem.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingSystem.DAL.Repositories
{
    public interface IAccountRepository
    {
        public Task<ICollection<Account>> GetAllAsync();
        public Task<Account> GetByIdAsync(int id);
        public Task<Account> GetByAccountNumberAsync(string accountNumber);
        public Task<Account> CreateAsync(Account account);
        public Task<Account> UpdateAsync(Account account);
        public Task<bool> DeleteAsync(int id);
    }
}
