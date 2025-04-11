using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayrollManagement.model
{
    internal class FinancialRecord
    {
        public int RecordID { get; set; }
        public int EmployeeID { get; set; }
        public DateTime RecordDate { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public string RecordType { get; set; }

        public override string ToString()
        {
            return $"RecordID::{RecordID}\t EmployeeID::{EmployeeID}\t RecordDate::{RecordDate}\t Description:{Description}\t Amount::{Amount}\t RecordType::{RecordType}";
        }

        public FinancialRecord()
        {
        }

        public FinancialRecord(int recordID, int employeeID, DateTime recordDate,
                             string description, decimal amount, string recordType)
        {
            RecordID = recordID;
            EmployeeID = employeeID;
            RecordDate = recordDate;
            Description = description;
            Amount = amount;
            RecordType = recordType;
        }
    }
}
