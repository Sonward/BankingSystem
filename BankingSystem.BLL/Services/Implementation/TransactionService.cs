using BankingSystem.BLL.Utils;
using BankingSystem.Common.Exceptions;
using BankingSystem.DAL.Entities;
using BankingSystem.DAL.Repositories;
using BankingSystem.DTO.EntityDTO;

namespace BankingSystem.BLL.Services.Implementation;

public class TransactionService
    (IAccountRepository accountRepository,
    ITransactionRepository transactionRepository) : ITransactionService
{
    public async Task<TransactionDTO> GetTransactionById(Guid id)
    {
        return CustomMapper.TransactionToDto(await transactionRepository.GetByIdAsync(id));
    }

    public async Task<TransactionDTO> DepositAsync(string targetNumber, decimal amount)
    {
        if (targetNumber is null)
        {
            throw new ArgumentNullException(nameof(targetNumber));
        }
        if (amount <= 0)
        {
            throw new ArgumentException("Deposit amount cannot be less than or equal to zero.");
        }

        var entity = await accountRepository.GetByAccountNumberAsync(targetNumber);
        entity.Balance += amount;
        await accountRepository.UpdateAsync(entity);

        var result = await transactionRepository.CreateAsync(new Transaction()
        {
            AccountNumber = targetNumber,
            Amount = amount,
            TransactinType = TransactionType.Deposit
        });

        return CustomMapper.TransactionToDto(result);
    }

    public async Task<TransactionDTO> WithdrawAsync(string targetNumber, decimal amount)
    {
        if (targetNumber is null)
        {
            throw new ArgumentNullException(nameof(targetNumber));
        }
        if (amount <= 0)
        {
            throw new ArgumentException("Withdraw amount cannot be less than or equal to zero.");
        }

        var entity = await accountRepository.GetByAccountNumberAsync(targetNumber);
        if (entity.Balance < amount) throw new ArgumentException("Balance is lower that withdrawing amount");

        entity.Balance -= amount;
        await accountRepository.UpdateAsync(entity);

        var result = await transactionRepository.CreateAsync(new Transaction()
        {
            AccountNumber = targetNumber,
            Amount = amount,
            TransactinType = TransactionType.Withdraw
        });

        return CustomMapper.TransactionToDto(result);
    }

    public async Task<TransferTransactionDTO> TransferAsync(string targetFromNumber, string targetToNumber, decimal amount)
    {
        if (targetFromNumber is null)
        {
            throw new ArgumentNullException(nameof(targetFromNumber));
        }
        if (targetToNumber is null)
        {
            throw new ArgumentNullException(nameof(targetToNumber));
        }
        if (amount <= 0)
        {
            throw new ArgumentException("Transfer amount cannot be less than or equal to zero.");
        }

        var entityFrom = await accountRepository.GetByAccountNumberAsync(targetFromNumber);
        var entityTo = await accountRepository.GetByAccountNumberAsync(targetToNumber);
        if (entityFrom.Balance < amount) throw new ArgumentException("Balance is lower that withdrawing amount");

        entityFrom.Balance -= amount;
        entityTo.Balance += amount;
        await accountRepository.UpdateAsync(entityFrom);
        await accountRepository.UpdateAsync(entityTo);

        var result = (TransferTransaction) await transactionRepository.CreateAsync(new TransferTransaction()
        {
            AccountNumber = targetFromNumber,
            Amount = amount,
            TransactinType = TransactionType.Transfer,
            TransferToAccountNumber = targetToNumber
        });

        return (TransferTransactionDTO) CustomMapper.TransactionToDto(result);
    }

    public async Task<ICollection<TransactionDTO>> GetByAccountNumber(string accountNumber)
    {
        return (await transactionRepository.GetByAccountNumberAsync(accountNumber))
            .Select(t => CustomMapper.TransactionToDto(t))
            .ToList();
    }
}
