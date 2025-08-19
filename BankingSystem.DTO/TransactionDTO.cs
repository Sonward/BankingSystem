using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingSystem.DTO
{
    public record TransactionDTO
        (int Id,
        string TargetAccountNumber, 
        decimal Amount, 
        DateTime CreationTime, 
        TransactinType TransactinType,
        TransactionStatus TransactionStatus);
}
