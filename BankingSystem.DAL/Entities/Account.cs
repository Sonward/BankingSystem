using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
