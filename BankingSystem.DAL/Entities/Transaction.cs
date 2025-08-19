using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingSystem.DAL.Entities
{
    public class Transaction
    {
        public int Id { get; set; }
        public string TargetAccountNumber {  get; set; }
        public decimal Amount { get; set; }
        public TransactinType TransactinType { get; set; }
        public TransactionStatus TransactionStatus { get; set; }
        public DateTime CreationTime { get; set; } = DateTime.UtcNow;
    }
}
