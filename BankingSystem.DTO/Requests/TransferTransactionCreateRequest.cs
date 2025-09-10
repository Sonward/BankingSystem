using BankingSystem.DTO.EntityDTO;

namespace BankingSystem.DTO.Requests;

public record TransferTransactionCreateRequest
{
    public TransferTransactionCreateRequest(string targetFromNumber, string targetToNumber, decimal amount)
    {
        TargetFromNumber = targetFromNumber;
        TargetToNumber = targetToNumber;
        Amount = amount;
    }

    public string TargetFromNumber { get; }
    public string TargetToNumber { get; }
    public decimal Amount { get; }
}
