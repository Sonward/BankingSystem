using BankingSystem.DTO.EntityDTO;

namespace BankingSystem.DTO.Requests;

public record TransferTransactionCreateRequest
{
    public TransferTransactionCreateRequest(AccountDTO targetFrom, AccountDTO targetTo, decimal amount)
    {
        TargetFrom = targetFrom;
        TargetTo = targetTo;
        Amount = amount;
    }

    public AccountDTO TargetFrom { get; }
    public AccountDTO TargetTo { get; }
    public decimal Amount { get; }
}
