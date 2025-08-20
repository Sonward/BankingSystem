namespace BankingSystem.DTO
{
    public record TransferTransactionDTO(int Id,
        string TargetAccountNumber,
        decimal Amount,
        DateTime CreationTime,
        TransactionType TransactinType,
        string TransferToAccountNumber);
}
