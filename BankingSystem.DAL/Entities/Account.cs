namespace BankingSystem.DAL.Entities
{
    public class Account
    {
        public int Id { get; set; }
        public string Number { get; set; } = Guid.NewGuid().ToString();
        public string OwnerName { get; set; }
        public decimal Balance { get; set; }
        public DateTime CreationTime { get; set; } = DateTime.UtcNow;
    }
}
