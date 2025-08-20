using BankingSystem.BLL.Utils;
using BankingSystem.DAL.Entities;
using BankingSystem.DAL.Repositories;
using BankingSystem.DTO;

namespace BankingSystem.BLL.Services.Implementation
{
    public class TransactionService
        (IAccountRepository accountRepository,
        ITransactionRepository transactionRepository) : ITransactionService
    {
        public async Task<TransactionDTO> DepositAsync(AccountDTO target, decimal amount)
        {
            if (target is null) throw new ArgumentNullException(nameof(target));

            var entity = await accountRepository.GetByAccountNumberAsync(target.Number);
            entity.Balance += amount;
            await accountRepository.UpdateAsync(entity);

            var result = await transactionRepository.CreateAsync(new Transaction()
            {
                AccountNumber = target.Number,
                Amount = amount,
                TransactinType = TransactionType.Deposit
            });

            return CustomMapper.TransactionToDto(result);
        }

        public async Task<TransactionDTO> WithdrawAsync(AccountDTO target, decimal amount)
        {
            if (target is null) throw new ArgumentNullException(nameof(target));

            var entity = await accountRepository.GetByAccountNumberAsync(target.Number);
            if (entity.Balance < amount) throw new ArgumentException("Balance is lower that withdrawing amount");

            entity.Balance -= amount;
            await accountRepository.UpdateAsync(entity);

            var result = await transactionRepository.CreateAsync(new Transaction()
            {
                AccountNumber = target.Number,
                Amount = amount,
                TransactinType = TransactionType.Withdraw
            });

            return CustomMapper.TransactionToDto(result);
        }

        public async Task<TransferTransactionDTO> TransferAsync(AccountDTO targetFrom, AccountDTO targetTo, decimal amount)
        {
            if (targetFrom is null) throw new ArgumentNullException(nameof(targetFrom));
            if (targetTo is null) throw new ArgumentNullException(nameof(targetTo));

            var entityFrom = await accountRepository.GetByAccountNumberAsync(targetFrom.Number);
            var entityTo = await accountRepository.GetByAccountNumberAsync(targetTo.Number);
            if (entityFrom.Balance < amount) throw new ArgumentException("Balance is lower that withdrawing amount");

            entityFrom.Balance -= amount;
            entityTo.Balance += amount;
            await accountRepository.UpdateAsync(entityFrom);
            await accountRepository.UpdateAsync(entityTo);

            var result = await transactionRepository.CreateAsync(new TransferTransaction()
            {
                AccountNumber = targetFrom.Number,
                Amount = amount,
                TransactinType = TransactionType.Transfer,
                TransferToAccountNumber = targetTo.Number
            }) as TransferTransaction;

            return CustomMapper.TransferTransactionToDto(result);
        }
    }
}
