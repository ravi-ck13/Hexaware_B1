using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayrollManagement.model
{
    public class Payroll
    {
        public int PayrollID { get; set; }
        public int EmployeeID { get; set; }
        public DateTime PayPeriodStartDate { get; set; }
        public DateTime PayPeriodEndDate { get; set; }
        public decimal BasicSalary { get; set; }
        public decimal OvertimePay { get; set; }
        public decimal Deductions { get; set; }
        public decimal NetSalary { get; set; }

        public override string ToString()
        {
            return $"PayRollId::{PayrollID}\t EmployeeID::{EmployeeID}\t PayPeriodStartDate::{PayPeriodStartDate}\t PayPeriodEndDate:{PayPeriodEndDate}\t BasicSalary::{BasicSalary}\t OvertimePay::{OvertimePay}\t Deductions::{Deductions}\t NetSalary::{NetSalary}";
        }

        public Payroll()
        {
        }

        public Payroll(int payrollID, int employeeID, DateTime payPeriodStartDate,
                      DateTime payPeriodEndDate, decimal basicSalary, decimal overtimePay,
                      decimal deductions, decimal netSalary)
        {
            PayrollID = payrollID;
            EmployeeID = employeeID;
            PayPeriodStartDate = payPeriodStartDate;
            PayPeriodEndDate = payPeriodEndDate;
            BasicSalary = basicSalary;
            OvertimePay = overtimePay;
            Deductions = deductions;
            NetSalary = netSalary;
        }
    }
}
