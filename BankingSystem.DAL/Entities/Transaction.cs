namespace BankingSystem.DAL.Entities;

public class Transaction
{
    public Guid Id { get; set; }
    public string AccountNumber {  get; set; }
    public decimal Amount { get; set; }
    public virtual TransactionType TransactinType { get; set; }
    public DateTime CreationTime { get; set; } = DateTime.UtcNow;
}
