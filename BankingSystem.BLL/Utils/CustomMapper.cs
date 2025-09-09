using BankingSystem.DAL.Entities;
using BankingSystem.DTO;
using BankingSystem.DTO.EntityDTO;

namespace BankingSystem.BLL.Utils
{
    internal static class CustomMapper
    {
        public static AccountDTO? AccountToDto(Account? account)
        {
            if (account is null) return null;

            return new AccountDTO
                (account.Id, 
                account.Number, 
                account.OwnerName, 
                account.Balance, 
                account.CreationTime);
        }

        public static TransactionDTO? TransactionToDto(Transaction? transaction)
        {
            if (transaction is null) return null;

            if (transaction is TransferTransaction transferTransaction)
            {
                return new TransferTransactionDTO
                    (transferTransaction.Id,
                    transferTransaction.AccountNumber,
                    transferTransaction.Amount, 
                    transferTransaction.CreationTime, 
                    transferTransaction.TransactinType, 
                    transferTransaction.TransferToAccountNumber);
            }
            return new TransactionDTO
                (transaction.Id, 
                transaction.AccountNumber, 
                transaction.Amount, 
                transaction.CreationTime, 
                transaction.TransactinType);
        }

    }
}
