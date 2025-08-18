using BankingSystem.DAL.Entities;
using BankingSystem.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingSystem.BLL.Services
{
    public interface IAccountService
    {
        public Task<Account> CreateAccount(AccountCreateRequest createRequest);
        public Task<Account> GetAccountByNumber(string number);
        public Task<ICollection<Account>> GetAllAccounts();
    }
}
