using BankingSystem.DTO.EntityDTO;

namespace BankingSystem.DTO.Requests;

public record TransactionCreateRequest
{
    public TransactionCreateRequest(string targetNumber, decimal amount)
    {
        TargetNumber = targetNumber;
        Amount = amount;
    }

    public string TargetNumber { get; }
    public decimal Amount { get; }
}
