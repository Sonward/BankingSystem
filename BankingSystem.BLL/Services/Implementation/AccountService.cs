using BankingSystem.BLL.Utils;
using BankingSystem.DAL.Entities;
using BankingSystem.DAL.Repositories;
using BankingSystem.DTO;

namespace BankingSystem.BLL.Services.Implementation
{
    public class AccountService(IAccountRepository accountRepository) : IAccountService
    {
        public async Task<AccountDTO> CreateAccountAsync(AccountCreateRequest createRequest)
        {
            if (createRequest is null) throw new ArgumentNullException(nameof(createRequest));
            if (string.IsNullOrWhiteSpace(createRequest.OwnerName))
                throw new ArgumentException("OwnerName must be provided.", nameof(createRequest.OwnerName));

            var result = await accountRepository.CreateAsync(new Account() 
            { 
                OwnerName = createRequest.OwnerName,
                Balance = createRequest.Balance
            });

            return CustomMapper.AccountToDto(result);
        }

        public async Task<AccountDTO> GetAccountByNumberAsync(string number)
        {
            if (string.IsNullOrWhiteSpace(number)) throw new ArgumentException("Account number must be provided.", nameof(number));
            
            return CustomMapper.AccountToDto(await accountRepository.GetByAccountNumberAsync(number));
        }

        public async Task<ICollection<AccountDTO>> GetAllAccountsAsync()
            => (await accountRepository.GetAllAsync()).Select(a => CustomMapper.AccountToDto(a)).ToList();
    }
}
