using BankingSystem.DTO;

namespace BankingSystem.BLL.Services
{
    public interface IAccountService
    {
        public Task<AccountDTO> CreateAccountAsync(AccountCreateRequest createRequest);
        public Task<AccountDTO> GetAccountByNumberAsync(string number);
        public Task<ICollection<AccountDTO>> GetAllAccountsAsync();
    }
}
