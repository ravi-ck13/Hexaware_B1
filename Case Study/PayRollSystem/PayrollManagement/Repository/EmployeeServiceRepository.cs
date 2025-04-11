using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using PayrollManagement.Exceptions;
using PayrollManagement.model;
using PayrollManagement.Utility;

namespace PayrollManagement.Repository
{
    public class EmployeeServiceRepository : IEmployeeServiceRepository
    {

        public string connectionString;
        SqlConnection sqlconnection = null;
        SqlCommand cmd = null;

        public EmployeeServiceRepository()
        {
            sqlconnection = new SqlConnection("Data Source=RAVI\\SQLEXPRESS;Initial Catalog=PayXpert;Integrated Security=True;TrustServerCertificate=True;");
            sqlconnection = new SqlConnection(DBConnUtil.GetConnectionString());
            connectionString = DBConnUtil.GetConnectionString();
            cmd = new SqlCommand();
        }
        public List<Employee> GetAllEmployees()
        {
            List<Employee> EmployeeList = new List<Employee>();
            try
            {
                using (SqlConnection sqlconnection = new SqlConnection(connectionString))
                {
                    cmd.CommandText = "select *from Employee";
                    cmd.Connection = sqlconnection;
                    sqlconnection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Employee employee = new Employee();
                        employee.EmployeeID = (int)reader["Employee_ID"];
                        employee.FirstName = (string)reader["First_Name"];
                        employee.LastName = (string)reader["last_Name"];
                        employee.DateOfBirth = (DateTime)reader["Date_Of_Birth"];
                        employee.Gender = (string)reader["Gender"];
                        employee.Mail = (string)reader["Email"];
                        employee.PhoneNumber = (string)reader["Phone_Number"];
                        employee.Address = (string)reader["Address"];
                        employee.Position = (string)reader["Position"];
                        employee.JoiningDate = (DateTime)reader["Joining_Date"];
                        if (reader["Termination_Date"] != DBNull.Value)
                        {
                            employee.TerminationDate = (DateTime)reader["Termination_Date"];
                        }
                        else
                        {
                            employee.TerminationDate = null;
                        }
                        EmployeeList.Add(employee);

                    }
                    cmd.Parameters.Clear();
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("DataBaseConnection failed" + ex.Message);
            }
            return EmployeeList;
        }
        public List<Employee> GetEmployeeById(int id)
        {
            List<Employee> EmployeeList = new List<Employee>();
            try
            {
                using (SqlConnection sqlconnection = new SqlConnection(connectionString))
                {
                    cmd.CommandText = "select *from Employee where Employee_ID=@EmployeeID";
                    cmd.Parameters.AddWithValue("@EmployeeID", id);
                    cmd.Connection = sqlconnection;
                    sqlconnection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (!reader.HasRows)
                    {
                        throw new EmployeeNotFoundException("Employee ID does not exist.");
                    }
                    while (reader.Read())
                    {
                        Employee emp = new Employee();
                        emp.EmployeeID = (int)reader["Employee_ID"];
                        emp.FirstName = (string)reader["First_Name"];
                        emp.LastName = (string)reader["last_Name"];
                        emp.DateOfBirth = (DateTime)reader["Date_Of_Birth"];
                        emp.Gender = (string)reader["Gender"];
                        emp.Mail = (string)reader["Email"];
                        emp.PhoneNumber = (string)reader["Phone_Number"];
                        emp.Address = (string)reader["Address"];
                        emp.Position = (string)reader["Position"];
                        emp.JoiningDate = (DateTime)reader["Joining_Date"];
                        if (reader["Termination_Date"] != DBNull.Value)
                        {
                            emp.TerminationDate = (DateTime)reader["Termination_Date"];
                        }
                        else
                        {
                            emp.TerminationDate = null;
                        }
                        EmployeeList.Add(emp);
                    }
                    cmd.Parameters.Clear();
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("DataBaseConnection failed" + ex.Message);
            }
            catch (EmployeeNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
            }
            return EmployeeList;
        }
        public int UpdateEmployee(Employee employee)
        {
            int UpdateStatus = 0;
            try
            {
                using (SqlConnection sqlconnection = new SqlConnection(connectionString))
                {
                    cmd.CommandText = "UPDATE employee SET First_Name=@FirstName where Employee_ID=@EmployeeID";
                    cmd.Parameters.AddWithValue("@FirstName", employee.FirstName);
                    cmd.Parameters.AddWithValue("@EmployeeID", employee.EmployeeID);
                    cmd.Connection = sqlconnection;
                    sqlconnection.Open();
                    UpdateStatus = cmd.ExecuteNonQuery();
                    cmd.Parameters.Clear();
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("DataBaseConnection failed" + ex.Message);
            }
            return UpdateStatus;
        }
        public int RemoveEmployee(int employeeId)
        {
            int DeleteStatus = 0;
            try
            {
                using (SqlConnection sqlconnection = new SqlConnection(connectionString))
                {
                    cmd.CommandText = "Delete from Employee where Employee_ID=@EmployeeID";
                    cmd.Parameters.AddWithValue("@EmployeeID", employeeId);
                    cmd.Connection = sqlconnection;
                    sqlconnection.Open();
                    DeleteStatus = cmd.ExecuteNonQuery();
                    cmd.Parameters.Clear();

                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("DataBaseConnection failed" + ex.Message);
            }
            return DeleteStatus;
        }
        public int AddEmployee(Employee employee)
        {
            int addEmployeeStatus = 0;
            try
            {
                using (SqlConnection sqlconnection = new SqlConnection(connectionString))
                {
                    cmd.CommandText = "INSERT INTO Employee (First_Name, Last_Name, Date_Of_Birth, Gender, Email, Phone_Number, Address, Position, Joining_Date) " +
                      "VALUES (@FirstName, @LastName, @DateOfBirth, @Gender, @Email, @PhoneNumber, @Address, @Position, @JoiningDate)";
                    
                    cmd.Parameters.AddWithValue("@FirstName", employee.FirstName);
                    cmd.Parameters.AddWithValue("@LastName", employee.LastName);
                    cmd.Parameters.AddWithValue("@DateOfBirth", employee.DateOfBirth);
                    cmd.Parameters.AddWithValue("@Gender", employee.Gender);
                    cmd.Parameters.AddWithValue("@Email", employee.Mail);
                    cmd.Parameters.AddWithValue("@PhoneNumber", employee.PhoneNumber);
                    cmd.Parameters.AddWithValue("@Address", employee.Address);
                    cmd.Parameters.AddWithValue("@Position", employee.Position);
                    cmd.Parameters.AddWithValue("@JoiningDate", employee.JoiningDate);
                    cmd.Connection = sqlconnection;
                    sqlconnection.Open();
                    addEmployeeStatus = cmd.ExecuteNonQuery();
                    cmd.Parameters.Clear();
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("DataBaseConnection failed" + ex.Message);
            }
            return addEmployeeStatus;
        }
    }
}
