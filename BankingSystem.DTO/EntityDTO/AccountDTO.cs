namespace BankingSystem.DTO.EntityDTO;

public record AccountDTO
{
    public AccountDTO
        (Guid id, 
        string number, 
        string ownerName, 
        decimal balance, 
        DateTime creationTime)
    {
        Id = id;
        Number = number;
        OwnerName = ownerName;
        Balance = balance;
        CreationTime = creationTime;
    }

    public Guid Id { get; }
    public string Number { get; }
    public string OwnerName { get; }
    public decimal Balance { get; }
    public DateTime CreationTime { get; }
}
