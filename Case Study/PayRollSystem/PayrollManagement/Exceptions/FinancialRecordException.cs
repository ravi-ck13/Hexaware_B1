using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayrollManagement.Exceptions
{
    internal class FinancialRecordException : ApplicationException
    {
        public FinancialRecordException()
        {

        }
        public FinancialRecordException(string message) : base(message)
        {

        }
    }
}
