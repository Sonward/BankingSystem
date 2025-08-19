using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingSystem.DTO.TransactionRequests
{
    public record TransferTransactionCreateRequest(AccountDTO TargetFrom, AccountDTO TargetTo, decimal Amount);
}
