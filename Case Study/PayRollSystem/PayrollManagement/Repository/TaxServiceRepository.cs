using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using PayrollManagement.model;
using PayrollManagement.Utility;

namespace PayrollManagement.Repository
{
    public class TaxServiceRepository : ITaxServiceRepository
    {
        public string connectionString;
        SqlConnection sqlconnection = null;
        SqlCommand cmd = null;
        public TaxServiceRepository()
        {
            sqlconnection = new SqlConnection("Data Source=RAVI\\SQLEXPRESS;Initial Catalog=PayXpert;Integrated Security=True;TrustServerCertificate=True;");
            
            connectionString = DBConnUtil.GetConnectionString();
            cmd = new SqlCommand();
        }
        public List<Tax> GetTaxById(int taxId)
        {
            List<Tax> taxList = new List<Tax>();
            try
            {
                using (SqlConnection sqlconnection = new SqlConnection(connectionString))
                {
                    cmd.CommandText = "Select *from Tax where Tax_ID=@taxID";
                    cmd.Parameters.AddWithValue("@TaxID", taxId);
                    cmd.Connection = sqlconnection;
                    sqlconnection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        Tax tax = new Tax();
                        tax.TaxID = (int)reader["Tax_ID"];
                        tax.EmployeeID = (int)reader["Employee_ID"];
                        tax.TaxYear = (int)reader["Tax_Year"];
                        tax.TaxableIncome = (decimal)reader["Taxable_Income"];
                        tax.TaxAmount = (decimal)reader["Tax_Amount"];
                        taxList.Add(tax);
                    }
                    cmd.Parameters.Clear();
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("DataBaseConnection failed" + ex.Message);
            }
            return taxList;
        }
        public List<Tax> GetTaxesForEmployee(int employeeId)
        {

            List<Tax> taxList = new List<Tax>();
            try
            {
                using (SqlConnection sqlconnection = new SqlConnection(connectionString))
                {
                    cmd.CommandText = "Select *from Tax where Employee_ID=@employeeID";
                    cmd.Parameters.AddWithValue("@employeeID", employeeId);
                    cmd.Connection = sqlconnection;
                    sqlconnection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Tax tax = new Tax();
                        tax.TaxID = (int)reader["Tax_ID"];
                        tax.EmployeeID = (int)reader["Employee_ID"];
                        tax.TaxYear = (int)reader["Tax_Year"];
                        tax.TaxableIncome = (decimal)reader["Taxable_Income"];
                        tax.TaxAmount = (decimal)reader["Tax_Amount"];
                        taxList.Add(tax);
                    }
                    cmd.Parameters.Clear();
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("DataBaseConnection failed" + ex.Message);
            }
            return taxList;
        }
        public List<Tax> GetTaxesForYear(int taxYear)
        {
            List<Tax> taxList = new List<Tax>();
            try
            {
                using (SqlConnection sqlconnection = new SqlConnection(connectionString))
                {
                    cmd.CommandText = "Select *from Tax where Tax_Year=@TaxYear";
                    cmd.Parameters.AddWithValue("@TaxYear", taxYear);
                    cmd.Connection = sqlconnection;
                    sqlconnection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Tax tax = new Tax();
                        tax.TaxID = (int)reader["Tax_ID"];
                        tax.EmployeeID = (int)reader["Employee_ID"];
                        tax.TaxYear = (int)reader["Tax_Year"];
                        tax.TaxableIncome = (decimal)reader["Taxable_Income"];
                        tax.TaxAmount = (decimal)reader["Tax_Amount"];
                        taxList.Add(tax);
                    }
                    cmd.Parameters.Clear();
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("DataBaseConnection failed" + ex.Message);
            }
            return taxList;
        }
        public decimal CalculateTax(int employeeId, int taxYear)
        {
            decimal taxAmount = 0;
            try
            {
                using (SqlConnection sqlconnection = new SqlConnection(connectionString))
                {
                    cmd.CommandText = "Select *from Tax where Employee_ID=@EmployeeId and Tax_Year=@TaxYear";
                    cmd.Parameters.AddWithValue("@EmployeeId", employeeId);
                    cmd.Parameters.AddWithValue("@TaxYear", taxYear);
                    cmd.Connection = sqlconnection;
                    sqlconnection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        decimal taxableIncome = (decimal)reader["Taxable_Income"];
                        taxAmount = taxableIncome * 0.10m;
                    }
                    cmd.Parameters.Clear();
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("DataBaseConnection failed" + ex.Message);
            }
            return taxAmount;
        }
    }
}
