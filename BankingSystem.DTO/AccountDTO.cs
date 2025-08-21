namespace BankingSystem.DTO
{
    public record AccountDTO
    {
        public AccountDTO
            (Guid id, 
            string number, 
            string ownerName, 
            decimal balamce, 
            DateTime creationTime)
        {
            Id = id;
            Number = number;
            OwnerName = ownerName;
            Balance = balamce;
            CreationTime = creationTime;
        }

        public Guid Id { get; }
        public string Number { get; }
        public string OwnerName { get; }
        public decimal Balance { get; }
        public DateTime CreationTime { get; }
    }
}
