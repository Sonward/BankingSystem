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
        public Task<AccountDTO> CreateAccountAsync(AccountCreateRequest createRequest);
        public Task<AccountDTO> GetAccountByNumberAsync(string number);
        public Task<ICollection<AccountDTO>> GetAllAccountsAsync();
    }
}
