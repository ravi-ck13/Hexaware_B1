using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PayrollManagement.Exceptions;
using PayrollManagement.model;
using PayrollManagement.Repository;

namespace PayrollManagement.Service
{
    internal class TaxService : ITaxService
    {
        readonly ITaxServiceRepository _taxServiceRepository;
        public TaxService()
        {
            _taxServiceRepository = new TaxServiceRepository();
        }
        public void GetTaxById()
        {
            Console.WriteLine("Enter the TaxId:");
            int taxID = int.Parse(Console.ReadLine());
            List<Tax> taxList = _taxServiceRepository.GetTaxById(taxID);
            foreach (Tax tax in taxList)
            {
                Console.WriteLine(tax);
            }
        }
        public void GetTaxesForEmployee()
        {
            Console.WriteLine("Enter the EmployeeId:");
            int Employeeno = int.Parse(Console.ReadLine());
            List<Tax> taxList = _taxServiceRepository.GetTaxesForEmployee(Employeeno);
            foreach (Tax tax in taxList)
            {
                Console.WriteLine(tax);
            }
        }
        public void GetTaxesForYear()
        {
            Console.WriteLine("Enter the TaxYear:");
            int TaxYear = int.Parse(Console.ReadLine());
            List<Tax> taxList = _taxServiceRepository.GetTaxesForYear(TaxYear);
            foreach (Tax tax in taxList)
            {
                Console.WriteLine(tax);
            }
        }
        public void CalculateTax()
        {
            try
            {
                Console.WriteLine("Enter the EmployeeId:");
                int Employeeno = int.Parse(Console.ReadLine());
                Console.WriteLine("Enter the TaxYear:");
                int TaxYear = int.Parse(Console.ReadLine());
                decimal tax = _taxServiceRepository.CalculateTax(Employeeno, TaxYear);
                if (tax > 0)
                {
                    Console.WriteLine($"The TaxAmount to be Paid by employeeID {Employeeno} is {tax}");
                }
                else
                {
                    throw new TaxCalculationException("Invalid EmployeeId or TaxYear");
                }
            }
            catch (TaxCalculationException ex)
            {
                Console.WriteLine("Tax Calculation is Failed" + ex.Message);
            }
        }

    }
}
