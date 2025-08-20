namespace BankingSystem.DTO.TransactionRequests
{
    public record TransactionCreateRequest(AccountDTO Target, decimal Amount);
}
