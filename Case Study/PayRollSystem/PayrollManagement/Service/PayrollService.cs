using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PayrollManagement.model;
using PayrollManagement.Repository;
using PayrollManagement.Exceptions;
using PayrollManagement.model;

namespace PayrollManagement.Service
{
    public class PayrollService : IPayrollService
    {
        readonly IPayrollServiceRepository _payrollServiceRepository;

        public PayrollService()
        {
            _payrollServiceRepository = new PayrollServiceRepository();
        }

       
        public void GetPayrollById()
        {
            Console.WriteLine("Enter the payrollId:");
            int payRollno = int.Parse(Console.ReadLine());
            List<Payroll> PayrollIdlist = _payrollServiceRepository.GetPayrollById(payRollno);
            foreach (Payroll payroll in PayrollIdlist)
            {
                Console.WriteLine(payroll);
            }
        }

        public void GetPayrollsForEmployee()
        {
            Console.WriteLine("Enter the EmployeeId:");
            int Employeeno = int.Parse(Console.ReadLine());
            List<Payroll> EmployeeIdlist = _payrollServiceRepository.GetPayrollsForEmployee(Employeeno);
            foreach (Payroll payroll in EmployeeIdlist)
            {
                Console.WriteLine(payroll);
            }
        }

   
        public void GetPayrollsForPeriod()
        {
            Console.WriteLine("Enter the startDate:");
            DateTime startdate = DateTime.Parse(Console.ReadLine());
            Console.WriteLine("Enter the endDate:");
            DateTime enddate = DateTime.Parse(Console.ReadLine());
            List<Payroll> PayRollsForPeriod = _payrollServiceRepository.GetPayrollsForPeriod(startdate, enddate);
            foreach (Payroll payroll in PayRollsForPeriod)
            {
                Console.WriteLine(payroll);
            }
        }

      
        public void GeneratePayroll()
        {
            Console.WriteLine("Enter the EmployeeId:");
            int Employeeno = int.Parse(Console.ReadLine());

            if (Employeeno <= 0)
            {
                throw new PayrollGenerationException("Invalid Employee ID provided.");
            }

            Console.WriteLine("Enter the startDate:");
            DateTime startdate = DateTime.Parse(Console.ReadLine());
            Console.WriteLine("Enter the endDate:");
            DateTime enddate = DateTime.Parse(Console.ReadLine());

            List<Payroll> PayRoll = _payrollServiceRepository.GeneratePayroll(Employeeno, startdate, enddate);

            if (PayRoll == null || PayRoll.Count == 0)
            {
                throw new PayrollGenerationException("No payroll data generated for the given input.");
            }

            foreach (Payroll payroll in PayRoll)
            {
                Console.WriteLine(payroll);
            }
        }

       
        public decimal CalculateGrossSalary(decimal basic, decimal overtime)
        {
            return basic + overtime;
        }

        public decimal CalculateNetSalary(decimal gross, decimal deductions)
        {
            return gross - deductions;
        }

       
        public decimal CalculateSalary(Employee employee)
        {
            if (employee == null)
                throw new ArgumentException("Employee cannot be null");

      
            return employee.BasicSalary + employee.OvertimePay - employee.Deductions;
        }


        public void ProcessPayrollBatch(List<int> employeeIds, DateTime startDate, DateTime endDate)
        {
        
            if (employeeIds == null || employeeIds.Count == 0)
            {
                throw new PayrollGenerationException("No employee IDs provided for batch processing.");
            }

            Console.WriteLine($"Starting payroll batch processing for {employeeIds.Count} employees between {startDate.ToShortDateString()} and {endDate.ToShortDateString()}...\n");

         
            foreach (int employeeId in employeeIds)
            {
                try
                {
                    var payrolls = _payrollServiceRepository.GeneratePayroll(employeeId, startDate, endDate);

                    if (payrolls == null || payrolls.Count == 0)
                    {
                        Console.WriteLine($"No payroll data found for Employee ID: {employeeId}");
                        continue;
                    }

                    Console.WriteLine($"Payroll for Employee ID {employeeId}:");
                    foreach (var payroll in payrolls)
                    {
                        Console.WriteLine(payroll);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($" Error processing payroll for Employee ID {employeeId}: {ex.Message}");
                }

                Console.WriteLine(); 
            }

            Console.WriteLine(" Payroll batch processing completed.\n");
        }

    }
}
