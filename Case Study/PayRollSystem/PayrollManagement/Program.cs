using PayrollManagement.Service;
using System.Linq;
namespace PayrollManagement
{
    internal class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("\n===== Payroll Management System =====");
                Console.WriteLine("1. Employee Service");
                Console.WriteLine("2. Payroll Service");
                Console.WriteLine("3. Tax Service");
                Console.WriteLine("4. FinancialRecord Service");
                Console.WriteLine("0. Exit");
                Console.Write("Enter your choice: ");

                if (!int.TryParse(Console.ReadLine(), out int serviceChoice))
                {
                    Console.WriteLine("Invalid input. Please enter a valid number.");
                    continue;
                }

                switch (serviceChoice)
                {
                    case 1:
                        RunEmployeeService();
                        break;
                    case 2:
                        RunPayrollService();
                        break;
                    case 3:
                        RunTaxService();
                        break;
                    case 4:
                        RunFinancialRecordService();
                        break;
                    case 0:
                        Console.WriteLine("Exiting application...");
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }

        static void RunEmployeeService()
        {
            IEmployeeService employeeService = new EmployeeService();
            while (true)
            {
                Console.WriteLine("\n--- Employee Service Menu ---");
                Console.WriteLine("1. Get Employee by ID");
                Console.WriteLine("2. Get All Employees");
                Console.WriteLine("3. Add Employee");
                Console.WriteLine("4. Update Employee");
                Console.WriteLine("5. Remove Employee");
                Console.WriteLine("0. Go Back");
                Console.Write("Enter your choice: ");

                if (!int.TryParse(Console.ReadLine(), out int choice))
                {
                    Console.WriteLine("Invalid input. Please enter a valid number.");
                    continue;
                }

                switch (choice)
                {
                    case 1:
                        employeeService.GetEmployeeById();
                        break;
                    case 2:
                        employeeService.GetAllEmployees();
                        break;
                    case 3:
                        employeeService.AddEmployee();
                        break;
                    case 4:
                        employeeService.UpdateEmployee();
                        break;
                    case 5:
                        employeeService.RemoveEmployee();
                        break;
                    case 0:
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }

        static void RunPayrollService()
        {
            IPayrollService payrollService = new PayrollService();
            while (true)
            {
                Console.WriteLine("\n--- Payroll Service Menu ---");
                Console.WriteLine("1. Generate Payroll");
                Console.WriteLine("2. Get Payroll By ID");
                Console.WriteLine("3. Get Payrolls For Employee");
                Console.WriteLine("4. Get Payroll For Period");
                Console.WriteLine("5. Process Payroll Batch");
                Console.WriteLine("0. Go Back");
                Console.Write("Enter your choice: ");

                if (!int.TryParse(Console.ReadLine(), out int choice))
                {
                    Console.WriteLine("Invalid input. Please enter a valid number.");
                    continue;
                }

                switch (choice)
                {
                    case 1:
                        payrollService.GeneratePayroll();
                        break;
                    case 2:
                        payrollService.GetPayrollById();
                        break;
                    case 3:
                        payrollService.GetPayrollsForEmployee();
                        break;
                    case 4:
                        payrollService.GetPayrollsForPeriod();
                        break;
                    case 5:
                        try
                        {
                            Console.Write("Enter comma-separated Employee IDs (e.g., 101,102): ");
                            string input = Console.ReadLine();

                            if (string.IsNullOrWhiteSpace(input))
                            {
                                Console.WriteLine("No input provided.");
                                break;
                            }

                            List<int> employeeIds = input.Split(',')
                                .Select(id => int.TryParse(id.Trim(), out int result) ? result : -1)
                                .Where(id => id != -1)
                                .ToList();

                            Console.Write("Enter Start Date (yyyy-mm-dd): ");
                            DateTime startDate = DateTime.Parse(Console.ReadLine());

                            Console.Write("Enter End Date (yyyy-mm-dd): ");
                            DateTime endDate = DateTime.Parse(Console.ReadLine());

                            payrollService.ProcessPayrollBatch(employeeIds, startDate, endDate);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Error: " + ex.Message);
                        }
                        break;
                    case 0:
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }

        static void RunFinancialRecordService()
        {
            IFinancialRecordService financialRecordService = new FinancialRecordService();
            while (true)
            {
                Console.WriteLine("\n--- Financial Record Service Menu ---");
                Console.WriteLine("1. Add Financial Record");
                Console.WriteLine("2. Get Financial Record by ID");
                Console.WriteLine("3. Get Financial Records for Employee");
                Console.WriteLine("4. Get Financial Records for Date");
                Console.WriteLine("0. Go Back");
                Console.Write("Enter your choice: ");

                if (!int.TryParse(Console.ReadLine(), out int choice))
                {
                    Console.WriteLine("Invalid input. Please enter a valid number.");
                    continue;
                }

                switch (choice)
                {
                    case 1:
                        financialRecordService.AddFinancialRecord();
                        break;
                    case 2:
                        financialRecordService.GetFinancialRecordById();
                        break;
                    case 3:
                        financialRecordService.GetFinancialRecordsForEmployee();
                        break;
                    case 4:
                        financialRecordService.GetFinancialRecordsForDate();
                        break;
                    case 0:
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }

        static void RunTaxService()
        {
            ITaxService taxService = new TaxService();
            while (true)
            {
                Console.WriteLine("\n--- Tax Service Menu ---");
                Console.WriteLine("1. Calculate Tax");
                Console.WriteLine("2. Get Tax by ID");
                Console.WriteLine("3. Get Tax for Employee");
                Console.WriteLine("4. Get Tax for Year");
                Console.WriteLine("0. Go Back");
                Console.Write("Enter your choice: ");

                if (!int.TryParse(Console.ReadLine(), out int choice))
                {
                    Console.WriteLine("Invalid input. Please enter a valid number.");
                    continue;
                }

                switch (choice)
                {
                    case 1:
                        taxService.CalculateTax();
                        break;
                    case 2:
                        taxService.GetTaxById();
                        break;
                    case 3:
                        taxService.GetTaxesForEmployee();
                        break;
                    case 4:
                        taxService.GetTaxesForYear();
                        break;
                    case 0:
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }
    }
}
