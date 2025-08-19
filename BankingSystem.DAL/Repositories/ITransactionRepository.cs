using BankingSystem.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingSystem.DAL.Repositories
{
    public interface ITransactionRepository
    {
        public Task<Transaction> CreateAsync(Transaction transaction);
        public Task<ICollection<Transaction>> GetByAccountNumber(string accountNumber);
    }
}
