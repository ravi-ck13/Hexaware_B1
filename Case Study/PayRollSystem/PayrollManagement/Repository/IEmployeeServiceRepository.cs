using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PayrollManagement.model;

namespace PayrollManagement.Repository
{
    internal interface IEmployeeServiceRepository
    {
        List<Employee> GetAllEmployees();
        List<Employee> GetEmployeeById(int id);
        int AddEmployee(Employee employee);
        int UpdateEmployee(Employee employee);
        int RemoveEmployee(int employeeId);
    }
}
