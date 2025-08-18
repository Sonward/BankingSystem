using BankingSystem.DAL.Entities;
using BankingSystem.DAL.Repositories;
using BankingSystem.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingSystem.BLL.Services.Implementation
{
    internal class AccountService(IAccountRepository accountRepository) : IAccountService
    {
        public async Task<Account> CreateAccount(AccountCreateRequest createRequest)
        {
            var result = await accountRepository.CreateAsync(new Account() 
            { 
                OwnerName = createRequest.OwnerName,
                Balance = createRequest.Balance
            });

            return result;
        }

        public async Task<Account> GetAccountByNumber(string number)
            => await accountRepository.GetByAccountNumberAsync(number);

        public async Task<ICollection<Account>> GetAllAccounts()
            => await accountRepository.GetAllAsync();
    }
}
