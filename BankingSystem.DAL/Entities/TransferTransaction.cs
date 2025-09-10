namespace BankingSystem.DAL.Entities;

public class TransferTransaction : Transaction
{
    public override TransactionType TransactinType 
    { 
        get => base.TransactinType;
        set
        {
            if (value != TransactionType.Transfer)
                throw new InvalidOperationException("TransferTransaction must be of type Transfer.");
            base.TransactinType = value;
        }
    }
    public string TransferToAccountNumber { get; set; }
}
