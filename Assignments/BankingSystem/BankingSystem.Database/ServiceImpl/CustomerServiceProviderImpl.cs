using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankingSystem.DatabaseConnectivity.Bean;
using BankingSystem.DatabaseConnectivity.Service;
using BankingSystem.DatabaseConnectivity.Util;
using Microsoft.Data.SqlClient;

namespace BankingSystem.DatabaseConnectivity.ServiceImpl
{
    public class CustomerServiceProviderImpl : ICustomerServiceProvider
    {
        public float Deposit(long accountNumber, float amount)
        {
            Console.WriteLine($"[Deposit] Account {accountNumber} credited with ₹{amount}");
            return 0;
        }

        public float Withdraw(long accountNumber, float amount)
        {
            Console.WriteLine($"[Withdraw] Account {accountNumber} debited with ₹{amount}");
            return 0;
        }

        public float GetAccountBalance(long accountNumber)
        {
            Console.WriteLine($"[GetAccountBalance] Account {accountNumber}");
            return 0;
        }

        public void Transfer(long fromAccountNumber, long toAccountNumber, float amount)
        {
            Console.WriteLine($"[Transfer] From {fromAccountNumber} to {toAccountNumber} amount ₹{amount}");
        }

        public virtual Account GetAccountDetails(long accountNumber)
        {
            Console.WriteLine($"[GetAccountDetails] Fetching account for {accountNumber}");
            Account acc = null;

            try
            {
                using (SqlConnection con = DBUtil.GetConnection())
                {
                    con.Open();
                    string query = "SELECT a.account_id, a.customer_id, a.account_type, a.balance, a.InterestRate, a.OverdraftLimit, " +
                                   "c.first_name, c.last_name, c.dob, c.email, c.phone_number, c.customer_address " +
                                   "FROM accounts a INNER JOIN customers c ON a.customer_id = c.customer_id " +
                                   "WHERE a.account_id = @accNo";

                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@accNo", accountNumber);

                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        // Map customer with safe casting
                        Customer customer = new Customer
                        {
                            CustomerId = Convert.ToInt64(reader.GetValue(1)),
                            FirstName = reader.GetString(6),
                            LastName = reader.GetString(7),
                            DOB = reader.GetDateTime(8),
                            Email = reader.GetString(9),
                            PhoneNumber = reader.GetString(10),
                            CustomerAddress = reader.GetString(11)
                        };

                        string accountType = reader.GetString(2);

                        // Instantiate correct account type based on 'account_type'
                        if (accountType.ToLower() == "savings")
                        {
                            acc = new SavingsAccount
                            {
                                InterestRate = reader.IsDBNull(4) ? 0 : reader.GetDecimal(4)
                            };
                        }
                        else if (accountType.ToLower() == "current")
                        {
                            acc = new CurrentAccount
                            {
                                OverdraftLimit = reader.IsDBNull(5) ? 0 : reader.GetDecimal(5)
                            };
                        }
                        else
                        {
                            acc = new ZeroBalanceAccount();
                        }

                        // Set common properties safely
                        acc.AccountNumber = Convert.ToInt32(reader.GetValue(0));
                        acc.AccountType = accountType;
                        acc.Balance = reader.GetDecimal(3);
                        acc.Customer = customer;

                        // Print details
                        Console.WriteLine("\n--- Account Details ---");
                        Console.WriteLine($"Account ID      : {acc.AccountNumber}");
                        Console.WriteLine($"Account Type    : {acc.AccountType}");
                        Console.WriteLine($"Balance         : ₹{acc.Balance}");
                        Console.WriteLine($"Customer Name   : {customer.FirstName} {customer.LastName}");
                        Console.WriteLine($"Email           : {customer.Email}");
                        Console.WriteLine($"Phone           : {customer.PhoneNumber}");
                        Console.WriteLine($"Address         : {customer.CustomerAddress}");

                        if (acc is SavingsAccount sa)
                        {
                            Console.WriteLine($"Interest Rate   : {sa.InterestRate}%");
                        }
                        else if (acc is CurrentAccount ca)
                        {
                            Console.WriteLine($"Overdraft Limit : ₹{ca.OverdraftLimit}");
                        }
                    }
                    else
                    {
                        Console.WriteLine("No account found with the given number.");
                    }

                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error fetching account: " + ex.Message);
            }

            return acc;
        }


        public List<Transaction> GetTransactions(long accountNumber, DateTime fromDate, DateTime toDate)
        {
            Console.WriteLine($"[GetTransactions] for {accountNumber} between {fromDate} and {toDate}");
            return new List<Transaction>();
        }
    }
}
