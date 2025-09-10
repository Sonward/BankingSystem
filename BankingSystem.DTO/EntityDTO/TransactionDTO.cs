namespace BankingSystem.DTO.EntityDTO;

public record TransactionDTO
{
    public TransactionDTO
        (Guid id, 
        string targetAccountNumber, 
        decimal amount, 
        DateTime creationTime, 
        TransactionType transactionType)
    {
        Id = id;
        TargetAccountNumber = targetAccountNumber;
        Amount = amount;
        CreationTime = creationTime;
        TransactionType = transactionType;
    }

    public Guid Id { get; }
    public string TargetAccountNumber { get; }
    public decimal Amount { get; }
    public DateTime CreationTime { get; }
    public TransactionType TransactionType { get; }
}
