using BankingSystem.DTO.EntityDTO;
using BankingSystem.DTO.Requests;

namespace BankingSystem.BLL.Services;

public interface IAccountService
{
    public Task<AccountDTO> CreateAccountAsync(AccountCreateRequest createRequest);
    public Task<AccountDTO> GetAccountById(Guid id);
    public Task<AccountDTO> GetAccountByNumberAsync(string number);
    public Task<ICollection<AccountDTO>> GetAllAccountsAsync();
}
