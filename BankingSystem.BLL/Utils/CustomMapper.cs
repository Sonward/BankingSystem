using BankingSystem.DAL.Entities;
using BankingSystem.DTO;

namespace BankingSystem.BLL.Utils
{
    internal static class CustomMapper
    {
        public static AccountDTO AccountToDto(Account account)
            => new AccountDTO(account.Id, account.Number, account.OwnerName, account.Balance, account.CreationTime);

        public static TransactionDTO TransactionToDto(Transaction transaction)
            => new TransactionDTO(transaction.Id, transaction.AccountNumber, transaction.Amount, transaction.CreationTime, transaction.TransactinType, transaction.TransactionStatus);
    }
}
