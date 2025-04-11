using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayrollManagement.Service
{
    internal interface IPayrollService
    {
        void GetPayrollById();
        void GetPayrollsForEmployee();
        void GetPayrollsForPeriod();
        void GeneratePayroll();
        void ProcessPayrollBatch(List<int> employeeIds, DateTime startDate, DateTime endDate);

    }
}
