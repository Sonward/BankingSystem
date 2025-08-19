using BankingSystem.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace BankingSystem.DAL.Repositories.Implementation
{
    public class TransactionRepository(AppDbContext dbContext) : ITransactionRepository
    {
        public async Task<Transaction> CreateAsync(Transaction transaction)
        {
            if (transaction is null) throw new ArgumentNullException(nameof(transaction));

            var result = await dbContext.Transactions.AddAsync(transaction);
            await dbContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<ICollection<Transaction>> GetByAccountNumber(string accountNumber)
        {
            if (string.IsNullOrWhiteSpace(accountNumber))
                throw new ArgumentException("Account number cannot be null or empty", nameof(accountNumber));

            return await dbContext.Transactions.Where(t => t.TargetAccountNumber == accountNumber).ToListAsync();
        }
    }
}
