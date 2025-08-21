using BankingSystem.DAL.Entities;
using BankingSystem.DAL.Exeptions;
using Microsoft.EntityFrameworkCore;

namespace BankingSystem.DAL.Repositories.Implementation
{
    public class AccountRepository(AppDbContext dbContext) : IAccountRepository
    {
        public async Task<ICollection<Account>> GetAllAsync()
        {
            return await dbContext.Accounts.ToListAsync();
        }

        public async Task<Account> GetByIdAsync(Guid id)
        {
            var result = await dbContext.Accounts.FindAsync(id);

            if (result is null)
            {
                throw new NotFoundDalException($"Cannot find Account with Id: {id}");
            }

            return result;
        }

        public async Task<Account?> GetByAccountNumberAsync(string accountNumber)
        {
            var result = await dbContext.Accounts.FirstOrDefaultAsync(a => a.Number == accountNumber);

            return result;
        }

        public async Task<Account> CreateAsync(Account account)
        {
            if (account is null)
            {
                throw new ArgumentNullException(nameof(account));
            }

            var result = await dbContext.Accounts.AddAsync(account);
            await dbContext.SaveChangesAsync();
            
            return result.Entity;
        }

        public async Task<Account> UpdateAsync(Account account)
        {
            var entity = await dbContext.Accounts.FindAsync(account.Id);
            if (entity is null)
            {
                throw new NotFoundDalException($"Cannot find Account with Id: {account.Id}");
            }

            dbContext.Accounts.Entry(entity).CurrentValues.SetValues(account);
            await dbContext.SaveChangesAsync();

            return entity;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await dbContext.Accounts.FindAsync(id);
            if (entity is null)
            {
                throw new NotFoundDalException($"Cannot find Account with Id: {id}");
            }
            dbContext.Accounts.Remove(entity);
            
            return await dbContext.SaveChangesAsync() > 0;
        }
    }
}
