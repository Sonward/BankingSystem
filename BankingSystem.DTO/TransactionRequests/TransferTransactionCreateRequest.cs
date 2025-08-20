namespace BankingSystem.DTO.TransactionRequests
{
    public record TransferTransactionCreateRequest(AccountDTO TargetFrom, AccountDTO TargetTo, decimal Amount);
}
