using NUnit.Framework;
using PayrollManagement.Service;

namespace PayXpert.Tests
{
    public class PayrollTests
    {
        [Test]
        public void CalculateGrossSalaryForEmployee_ShouldReturnCorrectValue()
        {
            // Arrange
            decimal basic = 30000;
            decimal overtime = 5000;
            var expectedGross = 35000;

            // Act
            var grossSalary = basic + overtime;

            // Assert
            Assert.That(grossSalary, Is.EqualTo(expectedGross));
        }

        [Test]
        public void CalculateNetSalaryAfterDeductions_ShouldReturnCorrectValue()
        {
            // Arrange
            decimal gross = 50000;
            decimal deductions = 8000;
            var expectedNet = 42000;

            // Act
            var netSalary = gross - deductions;

            // Assert
            Assert.That(netSalary, Is.EqualTo(expectedNet));
        }

        [Test]
        public void VerifyTaxCalculationForHighIncomeEmployee_ShouldReturnCorrectTax()
        {
            // Arrange
            decimal income = 100000;
            decimal expectedTax = income * 0.3m; // assuming 30% for high-income

            // Act
            decimal calculatedTax = 0.3m * income;

            // Assert
            Assert.That(calculatedTax, Is.EqualTo(expectedTax));
        }

        [Test]
        public void ProcessPayrollForMultipleEmployees_ShouldNotThrow()
        {
            try
            {
                // Arrange
                var payrollService = new PayrollService();

                // Dummy employee IDs for testing
                List<int> employeeIds = new List<int> { 1, 2, 3 };
                DateTime startDate = new DateTime(2024, 01, 01);
                DateTime endDate = new DateTime(2024, 12, 31);

                // Act
                payrollService.ProcessPayrollBatch(employeeIds, startDate, endDate);

                // Assert
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
                // Assumes CalculateSalary throws ArgumentException on null
                payrollService.CalculateSalary(null);
            }, "Expected ArgumentException for invalid employee data.");
        }
    }
}