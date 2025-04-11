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
    internal class FinancialRecordServiceRepository : IFinancialRecordServiceRepository
    {
        public string connectionString;
        SqlConnection sqlconnection = null;
        SqlCommand cmd = null;
        public FinancialRecordServiceRepository()
        {
            sqlconnection = new SqlConnection("Data Source=RAVI\\SQLEXPRESS;Initial Catalog=PayXpert;Integrated Security=True;TrustServerCertificate=True;");
            sqlconnection = new SqlConnection(DBConnUtil.GetConnectionString());
            connectionString = DBConnUtil.GetConnectionString();
            cmd = new SqlCommand();

        }
        public List<FinancialRecord> GetFinancialRecordById(int recordId)
        {
            List<FinancialRecord> financialRecordList = new List<FinancialRecord>();
            try
            {
                using (SqlConnection sqlconnection = new SqlConnection(connectionString))
                {
                    cmd.CommandText = "SELECT * FROM Financial_Record WHERE Record_ID=@RecordID";
                    cmd.Parameters.AddWithValue("@RecordID", recordId);
                    cmd.Connection = sqlconnection;
                    sqlconnection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        FinancialRecord financialRecord = new FinancialRecord();
                        financialRecord.RecordID = (int)reader["Record_ID"];
                        financialRecord.EmployeeID = (int)reader["Employee_ID"];
                        financialRecord.RecordDate = (DateTime)reader["Record_Date"];
                        financialRecord.Description = (string)reader["Description"];
                        financialRecord.Amount = (decimal)reader["Amount"];
                        financialRecord.RecordType = (string)reader["Record_Type"];
                        financialRecordList.Add(financialRecord);
                    }
                    else
                    {
                        throw new FinancialRecordException("FinancialRecord not found");
                    }
                    cmd.Parameters.Clear();
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("DataBaseConnection failed" + ex.Message);
            }
            catch (FinancialRecordException ex)
            {
                Console.WriteLine(ex.Message);
            }
            return financialRecordList;


        }
        public List<FinancialRecord> GetFinancialRecordsForEmployee(int employeeId)
        {
            List<FinancialRecord> financialRecordList = new List<FinancialRecord>();
            try
            {
                using (SqlConnection sqlconnection = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("SELECT * FROM Financial_Record WHERE Employee_ID = @EmployeeID", sqlconnection);
                    cmd.Parameters.AddWithValue("@EmployeeID", employeeId);

                    sqlconnection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    bool found = false;
                    while (reader.Read())
                    {
                        found = true;
                        FinancialRecord financialRecord = new FinancialRecord();
                        financialRecord.RecordID = (int)reader["Record_ID"];
                        financialRecord.EmployeeID = (int)reader["Employee_ID"];
                        financialRecord.RecordDate = (DateTime)reader["Record_Date"];
                        financialRecord.Description = reader["Description"].ToString();
                        financialRecord.Amount = (decimal)reader["Amount"];
                        financialRecord.RecordType = reader["Record_Type"].ToString();
                        financialRecordList.Add(financialRecord);
                    }

                    if (!found)
                    {
                        throw new FinancialRecordException("Financial records not found for the given EmployeeID.");
                    }

                    cmd.Parameters.Clear();
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("DataBaseConnection failed: " + ex.Message);
            }
            catch (FinancialRecordException ex)
            {
                Console.WriteLine(ex.Message);
            }

            return financialRecordList;
        }

        public List<FinancialRecord> GetFinancialRecordsForDate(DateTime recordDate)
        {
            List<FinancialRecord> financialRecordList = new List<FinancialRecord>();
            try
            {
                using (SqlConnection sqlconnection = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("SELECT * FROM Financial_Record WHERE Record_Date = @RecordDate", sqlconnection);
                    cmd.Parameters.AddWithValue("@RecordDate", recordDate);
                    sqlconnection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    bool found = false;
                    while (reader.Read())
                    {
                        found = true;
                        FinancialRecord financialRecord = new FinancialRecord();
                        financialRecord.RecordID = (int)reader["Record_ID"];
                        financialRecord.EmployeeID = (int)reader["Employee_ID"];
                        financialRecord.RecordDate = (DateTime)reader["Record_Date"];
                        financialRecord.Description = reader["Description"].ToString();
                        financialRecord.Amount = (decimal)reader["Amount"];
                        financialRecord.RecordType = reader["Record_Type"].ToString();
                        financialRecordList.Add(financialRecord);
                    }

                    if (!found)
                    {
                        throw new FinancialRecordException("No financial records found for the given date.");
                    }

                    cmd.Parameters.Clear();
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("DataBaseConnection failed: " + ex.Message);
            }
            catch (FinancialRecordException ex)
            {
                Console.WriteLine(ex.Message);
            }

            return financialRecordList;
        }

        public int AddFinancialRecord(FinancialRecord financialRecord)
        {
            int addRecordStatus = 0;
            try
            {
                using (SqlConnection sqlconnection = new SqlConnection(connectionString))
                {
                    cmd.CommandText = "INSERT INTO financial_record (employee_id, record_date, description, amount, record_type) VALUES (@EmployeeID, @RecordDate, @Description, @Amount, @RecordType)";
                   
                    cmd.Parameters.AddWithValue("@EmployeeID", financialRecord.EmployeeID);
                    cmd.Parameters.AddWithValue("@RecordDate", financialRecord.RecordDate);
                    cmd.Parameters.AddWithValue("@Description", financialRecord.Description);
                    cmd.Parameters.AddWithValue("@Amount", financialRecord.Amount);
                    cmd.Parameters.AddWithValue("@RecordType", financialRecord.RecordType);
                    cmd.Connection = sqlconnection;
                    sqlconnection.Open();
                    addRecordStatus = cmd.ExecuteNonQuery();
                    cmd.Parameters.Clear();
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("DataBaseConnection failed" + ex.Message);
            }
            return addRecordStatus;
        }
    }
}
