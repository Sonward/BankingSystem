namespace BankingSystem.DTO
{
    public record TransactionDTO
        (int Id,
        string TargetAccountNumber, 
        decimal Amount, 
        DateTime CreationTime, 
        TransactionType TransactinType);
}
