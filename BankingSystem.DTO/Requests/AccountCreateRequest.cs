namespace BankingSystem.DTO.Requests;

public record AccountCreateRequest
{
    public AccountCreateRequest(string ownerName, decimal balance)
    {
        OwnerName = ownerName;
        Balance = balance;
    }

    public string OwnerName { get; }
    public decimal Balance { get; }
};
