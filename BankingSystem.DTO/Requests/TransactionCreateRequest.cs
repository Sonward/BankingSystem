namespace BankingSystem.DTO.Requests
{
    public record TransactionCreateRequest
    {
        public TransactionCreateRequest(AccountDTO target, decimal amount)
        {
            Target = target;
            Amount = amount;
        }

        public AccountDTO Target { get; }
        public decimal Amount { get; }
    }
}
