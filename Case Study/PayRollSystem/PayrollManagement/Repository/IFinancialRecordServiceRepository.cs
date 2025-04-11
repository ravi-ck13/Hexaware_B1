using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PayrollManagement.model;

namespace PayrollManagement.Repository
{
    internal interface IFinancialRecordServiceRepository
    {
        List<FinancialRecord> GetFinancialRecordById(int recordId);
        List<FinancialRecord> GetFinancialRecordsForEmployee(int employeeId);
        List<FinancialRecord> GetFinancialRecordsForDate(DateTime recordDate);
        int AddFinancialRecord(FinancialRecord financialRecord);

    }
}
