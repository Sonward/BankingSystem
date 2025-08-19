using BankingSystem.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingSystem.BLL.Services
{
    public interface ITransactionService
    {
        public Task<TransactionDTO> DepositAsync(AccountDTO target, decimal amount);
        public Task<TransactionDTO> WithdrawAsync(AccountDTO target, decimal amount);
        public Task<ICollection<TransactionDTO>> TransferAsync(AccountDTO targetFrom, AccountDTO targetTo, decimal amount);
    }
}
