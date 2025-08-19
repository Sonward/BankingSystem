namespace BankingSystem.DTO
{
    public record AccountDTO(int Id, string Number, string OwnerName, decimal Balance, DateTime CreationTime);
}
