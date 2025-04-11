using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayrollManagement.model
{
    public class Tax
    {
        public int TaxID { get; set; }
        public int EmployeeID { get; set; }
        public int TaxYear { get; set; }
        public decimal TaxableIncome { get; set; }
        public decimal TaxAmount { get; set; }

        public override string ToString()
        {
            return $"TaxID: {TaxID}, EmployeeID: {EmployeeID}, TaxYear: {TaxYear}, " +
                   $"TaxableIncome: {TaxableIncome}, TaxAmount: {TaxAmount}";
        }

        public Tax() { }

        public Tax(int taxID, int employeeID, int taxYear, decimal taxableIncome, decimal taxAmount)
        {
            TaxID = taxID;
            EmployeeID = employeeID;
            TaxYear = taxYear;
            TaxableIncome = taxableIncome;
            TaxAmount = taxAmount;
        }
    }
}
