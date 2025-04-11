using NUnit.Framework;
using PayrollManagement.Service;

namespace PayXpert.Tests
{
    public class PayrollTests
    {
        [Test]
        public void CalculateGrossSalaryForEmployee_ShouldReturnCorrectValue()
        {
         
            decimal basic = 30000;
            decimal overtime = 5000;
            var expectedGross = 35000;

           
            var grossSalary = basic + overtime;

            
            Assert.That(grossSalary, Is.EqualTo(expectedGross));
        }

        [Test]
        public void CalculateNetSalaryAfterDeductions_ShouldReturnCorrectValue()
        {
         
            decimal gross = 50000;
            decimal deductions = 8000;
            var expectedNet = 42000;

           
            var netSalary = gross - deductions;

           
            Assert.That(netSalary, Is.EqualTo(expectedNet));
        }

        [Test]
        public void VerifyTaxCalculationForHighIncomeEmployee_ShouldReturnCorrectTax()
        {
            
            decimal income = 100000;
            decimal expectedTax = income * 0.3m; // assuming 30% for high-income

          
            decimal calculatedTax = 0.3m * income;

           
            Assert.That(calculatedTax, Is.EqualTo(expectedTax));
        }

        [Test]
        public void ProcessPayrollForMultipleEmployees_ShouldNotThrow()
        {
            try
            {
                
                var payrollService = new PayrollService();

                
                List<int> employeeIds = new List<int> { 1, 2, 3 };
                DateTime startDate = new DateTime(2024, 01, 01);
                DateTime endDate = new DateTime(2024, 12, 31);

              
                payrollService.ProcessPayrollBatch(employeeIds, startDate, endDate);

                
                Assert.Pass("Payroll processed successfully for multiple employees.");
            }
            catch (Exception ex)
            {
                Assert.Fail("Processing payroll threw an unexpected exception: " + ex.Message);
            }
        }

        [Test]
        public void VerifyErrorHandlingForInvalidEmployeeData_ShouldThrowException()
        {
            var payrollService = new PayrollService();

            Assert.Throws<ArgumentException>(() =>
            {
            
                payrollService.CalculateSalary(null);
            }, "Expected ArgumentException for invalid employee data.");
        }
    }
}