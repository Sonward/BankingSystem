using BankingSystem.DAL.Entities;
using BankingSystem.DAL.Exeptions;
using Microsoft.EntityFrameworkCore;

namespace BankingSystem.DAL.Repositories.Implementation
{
    public class TransactionRepository(AppDbContext dbContext) : ITransactionRepository
    {
        public async Task<Transaction> CreateAsync(Transaction transaction)
        {
            if (transaction is null)
            {
                throw new ArgumentNullException(nameof(transaction));
            }

            var result = await dbContext.Transactions.AddAsync(transaction);
            await dbContext.SaveChangesAsync();
            
            return result.Entity;
        }

        public async Task<Transaction> GetByIdAsync(Guid id)
        {
            var result = await dbContext.Transactions.FindAsync(id);

            if (result is null)
            {
                throw new NotFoundDalException($"Cannot find Account with Id: {id}");
            }

            return result;
        }

        public async Task<ICollection<Transaction>> GetByAccountNumberAsync(string accountNumber)
        {
            if (string.IsNullOrWhiteSpace(accountNumber))
            {
                throw new ArgumentException("Account number cannot be null or empty", nameof(accountNumber));
            }
            
            return await dbContext.Transactions.Where(t => t.AccountNumber == accountNumber).ToListAsync();
        }
    }
}
