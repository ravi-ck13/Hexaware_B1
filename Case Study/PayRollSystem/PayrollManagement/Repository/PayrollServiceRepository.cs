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
    public class PayrollServiceRepository : IPayrollServiceRepository
    {
        public string connectionString;
        SqlConnection sqlconnection = null;
        SqlCommand cmd = null;

        public PayrollServiceRepository()
        {
            sqlconnection = new SqlConnection("Data Source=RAVI\\SQLEXPRESS;Initial Catalog=PayXpert;Integrated Security=True;TrustServerCertificate=True;");
            
            connectionString = DBConnUtil.GetConnectionString();
            cmd = new SqlCommand();
        }

        public List<Payroll> GetPayrollById(int payrollId)
        {
            List<Payroll> payrollList = new List<Payroll>();
            try
            {
                using (SqlConnection sqlconnection = new SqlConnection(connectionString))
                {
                    cmd.CommandText = "Select *from Payroll where Payroll_ID=@PayrollID";
                    cmd.Parameters.AddWithValue("@PayrollID", payrollId);
                    cmd.Connection = sqlconnection;
                    sqlconnection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        Payroll payroll = new Payroll();
                        payroll.PayrollID = (int)reader["Payroll_ID"];
                        payroll.EmployeeID = (int)reader["Employee_ID"];
                        payroll.PayPeriodEndDate = (DateTime)reader["Pay_Period_End_Date"];
                        payroll.BasicSalary = (decimal)reader["Basic_Salary"];
                        payroll.OvertimePay = (decimal)reader["Overtime_Pay"];
                        payroll.Deductions = (decimal)reader["Deductions"];
                        payroll.NetSalary = (decimal)reader["Net_Salary"];
                        payrollList.Add(payroll);

                    }
                    else
                    {
                        // If no records found, throw an exception
                        throw new PayrollGenerationException("Payroll not generated for the specified PayrollID.");
                    }
                    cmd.Parameters.Clear();
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("DataBaseConnection failed" + ex.Message);
            }
            catch (PayrollGenerationException ex)
            {
                Console.WriteLine("Payroll generation failed: " + ex.Message);
            }
            return payrollList;
        }
        public List<Payroll> GetPayrollsForEmployee(int employeeId)
        {
            List<Payroll> payrollsList = new List<Payroll>();
            try
            {
                using (SqlConnection sqlconnection = new SqlConnection(connectionString))
                {
                    cmd.CommandText = "Select *from Payroll where Employee_ID=@EmployeeID";
                    cmd.Parameters.AddWithValue("@EmployeeID", employeeId);
                    cmd.Connection = sqlconnection;
                    sqlconnection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        while (reader.Read())
                        {
                            Payroll payroll = new Payroll();
                            payroll.PayrollID = (int)reader["Payroll_ID"];
                            payroll.EmployeeID = (int)reader["Employee_ID"];
                            payroll.PayPeriodEndDate = (DateTime)reader["Pay_Period_End_Date"];
                            payroll.BasicSalary = (decimal)reader["Basic_Salary"];
                            payroll.OvertimePay = (decimal)reader["Overtime_Pay"];
                            payroll.Deductions = (decimal)reader["Deductions"];
                            payroll.NetSalary = (decimal)reader["Net_Salary"];
                            payrollsList.Add(payroll);
                        }
                    }
                    else
                    {
                        // If no records found, throw an exception
                        throw new PayrollGenerationException("Payroll not generated for the specified EmployeeID.");
                    }
                    cmd.Parameters.Clear();

                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("DataBaseConnection failed" + ex.Message);
            }
            catch (PayrollGenerationException ex)
            {
                Console.WriteLine("Payroll generation failed: " + ex.Message);
            }
            return (payrollsList);
        }
        public List<Payroll> GetPayrollsForPeriod(DateTime startDate, DateTime endDate)
        {
            List<Payroll> payrollsListForPeriod = new List<Payroll>();
            try
            {
                using (SqlConnection sqlconnection = new SqlConnection(connectionString))
                {
                    cmd.CommandText = "Select *from Payroll where Pay_Period_Start_Date>=@PayPeriodStartDate and Pay_Period_End_Date<=@PayPeriodEndDate";
                    cmd.Parameters.AddWithValue("@PayPeriodStartDate", startDate);
                    cmd.Parameters.AddWithValue("@PayPeriodEndDate", endDate);
                    cmd.Connection = sqlconnection;
                    sqlconnection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            Payroll payroll = new Payroll();
                            payroll.PayrollID = (int)reader["Payroll_ID"];
                            payroll.EmployeeID = (int)reader["Employee_ID"];
                            payroll.PayPeriodEndDate = (DateTime)reader["Pay_Period_End_Date"];
                            payroll.BasicSalary = (decimal)reader["Basic_Salary"];
                            payroll.OvertimePay = (decimal)reader["Overtime_Pay"];
                            payroll.Deductions = (decimal)reader["Deductions"];
                            payroll.NetSalary = (decimal)reader["Net_Salary"];
                            payrollsListForPeriod.Add(payroll);
                        }
                    }
                    else
                    {
                        throw new PayrollGenerationException("Payroll is not generated for the specified Payperiod");
                    }
                    cmd.Parameters.Clear();
                }

            }
            catch (SqlException ex)
            {
                Console.WriteLine("DataBaseConnection failed" + ex.Message);
            }
            catch (PayrollGenerationException ex)
            {
                Console.WriteLine("Payroll generation failed: " + ex.Message);
            }
            return (payrollsListForPeriod);
        }
        public List<Payroll> GeneratePayroll(int employeeId, DateTime startDate, DateTime endDate)
        {
            List<Payroll> payrollList = new List<Payroll>();
            try
            {
                using (SqlConnection sqlconnection = new SqlConnection(connectionString))
                {
                    cmd.CommandText = "Select *from Payroll where Employee_ID=@EmployeeID and Pay_Period_Start_Date>=@PayPeriodStartDate and Pay_Period_End_Date<=@PayPeriodEndDate";
                    cmd.Parameters.AddWithValue("@EmployeeID", employeeId);
                    cmd.Parameters.AddWithValue("@PayPeriodStartDate", startDate);
                    cmd.Parameters.AddWithValue("@PayPeriodEndDate", endDate);
                    cmd.Connection = sqlconnection;
                    sqlconnection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        Payroll payroll = new Payroll();
                        payroll.PayrollID = (int)reader["Payroll_ID"];
                        payroll.EmployeeID = (int)reader["Employee_ID"];
                        payroll.PayPeriodEndDate = (DateTime)reader["Pay_Period_End_Date"];
                        payroll.BasicSalary = (decimal)reader["Basic_Salary"];
                        payroll.OvertimePay = (decimal)reader["Overtime_Pay"];
                        payroll.Deductions = (decimal)reader["Deductions"];
                        payroll.NetSalary = (decimal)reader["Net_Salary"];
                        payrollList.Add(payroll);
                    }
                    else
                    {
                        throw new PayrollGenerationException("Payroll is not generated for the specified condition");
                    }
                    cmd.Parameters.Clear();
                }

            }
            catch (SqlException ex)
            {
                Console.WriteLine("DataBaseConnection failed" + ex.Message);
            }
            catch (PayrollGenerationException ex)
            {
                Console.WriteLine("Payroll generation failed: " + ex.Message);
            }
            return (payrollList);

        }
    }
}
