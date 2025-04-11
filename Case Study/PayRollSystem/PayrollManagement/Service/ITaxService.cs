using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayrollManagement.Service
{
    internal interface ITaxService
    {
        void GetTaxById();
        void GetTaxesForEmployee();
        void GetTaxesForYear();
        void CalculateTax();
    }
}
