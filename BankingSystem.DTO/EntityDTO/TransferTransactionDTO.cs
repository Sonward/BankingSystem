namespace BankingSystem.DTO.EntityDTO
{
    public record TransferTransactionDTO : TransactionDTO
    {
        public TransferTransactionDTO
            (Guid id,
            string targetAccountNumber,
            decimal amount,
            DateTime creationTime,
            TransactionType transactionType,
            string transferToAccountNumber) : base(id,targetAccountNumber, amount, creationTime, transactionType)
        {
            TransferToAccountNumber = transferToAccountNumber;
        }

        public string TransferToAccountNumber { get; }
    }
}
