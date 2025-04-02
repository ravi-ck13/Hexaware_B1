using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UDException
{
    public class InsufficientFundsException : Exception
    {
        public double CurrentBalance { get; }

        public InsufficientFundsException(string message, double currentBalance): base(message)
        {
            CurrentBalance = currentBalance;
        }
    }
}
