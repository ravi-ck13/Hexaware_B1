using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PayrollManagement.Exceptions;
using PayrollManagement.model;
using PayrollManagement.Repository;
using System.Threading.Channels;

namespace PayrollManagement.Service
{
    internal class EmployeeService : IEmployeeService
    {
        readonly IEmployeeServiceRepository _employeeServiceRepository;
        public EmployeeService()
        {
            _employeeServiceRepository = new EmployeeServiceRepository();
        }
        public void GetAllEmployees()
        {
            List<Employee> allEmployees = _employeeServiceRepository.GetAllEmployees();

            foreach (Employee employee in allEmployees)
            {
                Console.WriteLine(employee);
            }
        }

        public void GetEmployeeById()
        {
            Console.WriteLine("Enter  EmployeeID::");
            int EmployeeID = int.Parse(Console.ReadLine());
            var getEmployee = _employeeServiceRepository.GetEmployeeById(EmployeeID);
            foreach (var employee in getEmployee)
            {
                Console.WriteLine(employee);
            }
        }
        public void UpdateEmployee()
        {
            try
            {
                Employee employee = new Employee();
                Console.WriteLine("Enter  EmployeeID::");
                employee.EmployeeID = int.Parse(Console.ReadLine());
                Console.WriteLine("Enter EmployeeFirstName:");
                employee.FirstName = Console.ReadLine();
               
                int UpdateStatus = _employeeServiceRepository.UpdateEmployee(employee);
                if (UpdateStatus > 0)
                {
                    Console.WriteLine("Employee details Updated Successfully");
                }
                else
                {
                    throw new EmployeeNotFoundException("EmployeeId not Found");
                }
            }
            catch (EmployeeNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
        public void RemoveEmployee()
        {
            try
            {
                Console.WriteLine("Enter  EmployeeID::");
                int EmployeeID = int.Parse(Console.ReadLine());
                int RemoveemployeeStatus = _employeeServiceRepository.RemoveEmployee(EmployeeID);
                if (RemoveemployeeStatus > 0)
                {
                    Console.WriteLine("Employee Record deleted successfully");
                }
                else
                {
                    throw new EmployeeNotFoundException("EmployeeId not found");
                }
            }
            catch (EmployeeNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public void AddEmployee()
        {
            try
            {
                Employee employee = new Employee();
                
                Console.WriteLine("Enter FirstName:");
                employee.FirstName = Console.ReadLine();
                Console.WriteLine("Enter LastName:");
                employee.LastName = Console.ReadLine();
                Console.WriteLine("Enter DateOfBirth (YYYY-MM-DD):");
                employee.DateOfBirth = DateTime.Parse(Console.ReadLine());
                Console.WriteLine("Enter Gender:");
                employee.Gender = Console.ReadLine();
                Console.WriteLine("Enter Email:");
                employee.Mail = Console.ReadLine();
                Console.WriteLine("Enter PhoneNumber:");
                employee.PhoneNumber = Console.ReadLine();
                Console.WriteLine("Enter Address:");
                employee.Address = Console.ReadLine();
                Console.WriteLine("Enter Position:");
                employee.Position = Console.ReadLine();
                Console.WriteLine("Enter JoiningDate (YYYY-MM-DD):");
                employee.JoiningDate = DateTime.Parse(Console.ReadLine());
                int AddUpdateStatus = _employeeServiceRepository.AddEmployee(employee);
                if (AddUpdateStatus > 0)
                {
                    Console.WriteLine("Employee added successfully.");
                }
                else
                {
                    throw new EmployeeNotFoundException("EmployeeId not found");
                }
            }
            catch (EmployeeNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
