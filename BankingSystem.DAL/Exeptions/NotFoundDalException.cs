using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingSystem.DAL.Exeptions
{
    internal class NotFoundDalException : Exception
    {
        public NotFoundDalException(string message) : base(message) { }
    }
}
