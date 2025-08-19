using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingSystem.DTO.TransactionRequests
{
    public record TransactionCreateRequest(AccountDTO Target, decimal Amount);
}
