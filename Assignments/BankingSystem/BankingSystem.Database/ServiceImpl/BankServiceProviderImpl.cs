using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankingSystem.DatabaseConnectivity.Bean;
using BankingSystem.DatabaseConnectivity.Service;
using BankingSystem.DatabaseConnectivity.Exceptions;
using BankingSystem.DatabaseConnectivity.Util;
using Microsoft.Data.SqlClient;

namespace BankingSystem.DatabaseConnectivity.ServiceImpl
{
    public class BankServiceProviderImpl : CustomerServiceProviderImpl, IBankServiceProvider
    {
        private readonly string connectionString = @"Data Source=RAVI\SQLEXPRESS;Initial Catalog=HMBank;Integrated Security=True;TrustServerCertificate=True;";

        private string branchName;
        private string branchAddress;

        public BankServiceProviderImpl(string branchName, string branchAddress)
        {
            this.branchName = branchName;
            this.branchAddress = branchAddress;
        }

        public Customer GetCustomerById(long customerId)
        {
            Customer customer = null;
            string query = "SELECT customer_id, first_name, last_name, dob, email, phone_number, customer_address FROM customers WHERE customer_id = @customerId";

            using (SqlConnection conn = DBUtil.GetConnection())
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@customerId", customerId);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            customer = new Customer
                            {
                                CustomerId = reader.GetInt32(0),
                                FirstName = reader.GetString(1),
                                LastName = reader.GetString(2),
                                DOB = reader.GetDateTime(3),
                                Email = reader.GetString(4),
                                PhoneNumber = reader.GetString(5),
                                CustomerAddress = reader.GetString(6)
                            };
                        }
                    }
                }
            }

            return customer;
        }

        private static long lastAccNo = 1000;

        public void CreateAccount(Customer customer, string accType, decimal balance)
        {
            long accNo = ++lastAccNo;

            string insertQuery = "INSERT INTO accounts (customer_id, account_type, balance, InterestRate, OverdraftLimit) " +
                                 "VALUES (@CustomerId, @AccountType, @Balance, @InterestRate, @OverdraftLimit)";

            using (SqlConnection conn = DBUtil.GetConnection())
            {
                SqlCommand cmd = new SqlCommand(insertQuery, conn);

                cmd.Parameters.AddWithValue("@CustomerId", customer.CustomerId);
                cmd.Parameters.AddWithValue("@AccountType", accType);
                cmd.Parameters.AddWithValue("@Balance", balance);

                if (accType.ToLower() == "savings")
                {
                    cmd.Parameters.AddWithValue("@InterestRate", 0.05m);
                    cmd.Parameters.AddWithValue("@OverdraftLimit", DBNull.Value);
                }
                else if (accType.ToLower() == "current")
                {
                    cmd.Parameters.AddWithValue("@InterestRate", DBNull.Value);
                    cmd.Parameters.AddWithValue("@OverdraftLimit", 10000m);
                }
                else if (accType.ToLower() == "zerobalance")
                {
                    cmd.Parameters.AddWithValue("@InterestRate", DBNull.Value);
                    cmd.Parameters.AddWithValue("@OverdraftLimit", DBNull.Value);
                }

                conn.Open();
                int rowsAffected = cmd.ExecuteNonQuery();

                Console.WriteLine(rowsAffected > 0
                    ? $"Account successfully created! Your Account Number is: {accNo}"
                    : "Failed to create account.");
            }
        }

        public List<Account> ListAccounts()
        {
            List<Account> accountList = new List<Account>();

            string selectQuery = "SELECT a.account_id, a.customer_id, a.account_type, a.balance, a.InterestRate, a.OverdraftLimit, " +
                                 "c.first_name, c.last_name, c.customer_address, c.phone_number " +
                                 "FROM accounts a " +
                                 "JOIN customers c ON a.customer_id = c.customer_id";

            using (SqlConnection conn = DBUtil.GetConnection())
            {
                SqlCommand cmd = new SqlCommand(selectQuery, conn);
                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Customer customer = new Customer
                    {
                        CustomerId = Convert.ToInt64(reader["customer_id"]),
                        FirstName = reader["first_name"].ToString(),
                        LastName = reader["last_name"].ToString(),
                        CustomerAddress = reader["customer_address"].ToString(),
                        PhoneNumber = reader["phone_number"].ToString()
                    };

                    string accType = reader["account_type"].ToString();
                    long accNo = Convert.ToInt64(reader["account_id"]);
                    decimal balance = Convert.ToDecimal(reader["balance"]);

                    Account account = null;

                    if (accType.ToLower() == "savings")
                    {
                        decimal interestRate = reader["InterestRate"] == DBNull.Value ? 0 : Convert.ToDecimal(reader["InterestRate"]);
                        account = new SavingsAccount
                        {
                            AccountNumber = (int)accNo,
                            AccountType = "Savings",
                            Balance = balance,
                            Customer = customer,
                            InterestRate = interestRate
                        };
                    }
                    else if (accType.ToLower() == "current")
                    {
                        decimal overdraftLimit = reader["OverdraftLimit"] == DBNull.Value ? 0 : Convert.ToDecimal(reader["OverdraftLimit"]);
                        account = new CurrentAccount
                        {
                            AccountNumber = (int)accNo,
                            AccountType = "Current",
                            Balance = balance,
                            Customer = customer,
                            OverdraftLimit = overdraftLimit
                        };
                    }
                    else if (accType.ToLower() == "zerobalance")
                    {
                        account = new ZeroBalanceAccount
                        {
                            AccountNumber = (int)accNo,
                            AccountType = "ZeroBalance",
                            Balance = balance,
                            Customer = customer
                        };
                    }

                    // Just in case account creation fails for any reason (safety check)
                    if (account != null)
                    {
                        accountList.Add(account);
                    }
                }

                reader.Close();
            }

            return accountList;
        }


        public void Deposit(long accNo, decimal amount, string description)
        {
            string updateBalanceQuery = "UPDATE accounts SET balance = balance + @amount WHERE account_id = @accNo";
            string insertTransactionQuery = "INSERT INTO transactions (account_id, transaction_type, amount, transaction_date, Description) " +
                                            "VALUES (@accNo, 'Deposit', @amount, @date, @desc)";

            using (SqlConnection conn = DBUtil.GetConnection())
            {
                conn.Open();
                SqlTransaction transaction = conn.BeginTransaction();

                try
                {
                    using (SqlCommand cmdUpdate = new SqlCommand(updateBalanceQuery, conn, transaction))
                    {
                        cmdUpdate.Parameters.AddWithValue("@amount", amount);
                        cmdUpdate.Parameters.AddWithValue("@accNo", accNo);
                        cmdUpdate.ExecuteNonQuery();
                    }

                    using (SqlCommand cmdInsert = new SqlCommand(insertTransactionQuery, conn, transaction))
                    {
                        cmdInsert.Parameters.AddWithValue("@accNo", accNo);
                        cmdInsert.Parameters.AddWithValue("@amount", amount);
                        cmdInsert.Parameters.AddWithValue("@date", DateTime.Now);
                        cmdInsert.Parameters.AddWithValue("@desc", description);
                        cmdInsert.ExecuteNonQuery();
                    }

                    transaction.Commit();
                    Console.WriteLine($"Amount ₹{amount} deposited successfully into account {accNo}");
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    Console.WriteLine("Error during deposit: " + ex.Message);
                }
            }
        }

        public decimal Withdraw(long accNo, decimal amount, string description)
        {
            string fetchQuery = "SELECT account_type, balance, OverdraftLimit FROM accounts WHERE account_id = @accNo";
            string updateQuery = "UPDATE accounts SET balance = balance - @amount WHERE account_id = @accNo";
            string insertTransactionQuery = "INSERT INTO transactions (account_id, transaction_type, amount, transaction_date, Description) " +
                                            "VALUES (@accNo, 'Withdraw', @amount, @date, @desc)";
            string fetchUpdatedBalanceQuery = "SELECT balance FROM accounts WHERE account_id = @accNo";

            using (SqlConnection conn = DBUtil.GetConnection())
            {
                conn.Open();
                SqlTransaction transaction = conn.BeginTransaction();

                try
                {
                    decimal currentBalance = 0;
                    decimal overdraftLimit = 0;
                    string accType = "";

                    using (SqlCommand cmdFetch = new SqlCommand(fetchQuery, conn, transaction))
                    {
                        cmdFetch.Parameters.AddWithValue("@accNo", accNo);
                        using (SqlDataReader reader = cmdFetch.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                accType = reader.GetString(0);
                                currentBalance = Convert.ToDecimal(reader["balance"]);
                                if (!reader.IsDBNull(2))
                                    overdraftLimit = Convert.ToDecimal(reader["OverdraftLimit"]);
                            }
                            else
                            {
                                Console.WriteLine("Account not found.");
                                transaction.Rollback();
                                return -1;
                            }
                        }
                    }

                    bool canWithdraw = accType.ToLower() == "current"
                        ? (currentBalance + overdraftLimit) >= amount
                        : currentBalance >= amount;

                    if (!canWithdraw)
                    {
                        Console.WriteLine("Insufficient balance or overdraft limit exceeded.");
                        transaction.Rollback();
                        return -1;
                    }

                    using (SqlCommand cmdUpdate = new SqlCommand(updateQuery, conn, transaction))
                    {
                        cmdUpdate.Parameters.AddWithValue("@amount", amount);
                        cmdUpdate.Parameters.AddWithValue("@accNo", accNo);
                        cmdUpdate.ExecuteNonQuery();
                    }

                    using (SqlCommand cmdInsert = new SqlCommand(insertTransactionQuery, conn, transaction))
                    {
                        cmdInsert.Parameters.AddWithValue("@accNo", accNo);
                        cmdInsert.Parameters.AddWithValue("@amount", amount);
                        cmdInsert.Parameters.AddWithValue("@date", DateTime.Now);
                        cmdInsert.Parameters.AddWithValue("@desc", description);
                        cmdInsert.ExecuteNonQuery();
                    }

                    // Commit before fetching updated balance
                    transaction.Commit();

                    // Fetch updated balance
                    using (SqlCommand cmdFetchUpdated = new SqlCommand(fetchUpdatedBalanceQuery, conn))
                    {
                        cmdFetchUpdated.Parameters.AddWithValue("@accNo", accNo);
                        object result = cmdFetchUpdated.ExecuteScalar();
                        if (result != null)
                        {
                            decimal updatedBalance = Convert.ToDecimal(result);
                            Console.WriteLine($"Amount ₹{amount} withdrawn successfully from account {accNo}");
                            return updatedBalance;
                        }
                        else
                        {
                            Console.WriteLine("Unable to retrieve updated balance.");
                            return -1;
                        }
                    }
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    Console.WriteLine("Error during withdrawal: " + ex.Message);
                    return -1;
                }
            }
        }


        public void ViewTransactionsForAccount(long accNo)
        {
            string query = "SELECT transaction_id, transaction_type, amount, transaction_date, Description " +
                           "FROM transactions WHERE account_id = @accNo ORDER BY transaction_date DESC";

            using (SqlConnection conn = DBUtil.GetConnection())
            {
                try
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@accNo", accNo);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            Console.WriteLine($"\nTransaction History for Account: {accNo}");
                            Console.WriteLine("---------------------------------------------------");

                            if (!reader.HasRows)
                            {
                                Console.WriteLine("No transactions found for this account.");
                                return;
                            }

                            while (reader.Read())
                            {
                                // Use Convert.ToInt64 to safely handle int/bigint
                                long txnId = Convert.ToInt64(reader["transaction_id"]);
                                string type = Convert.ToString(reader["transaction_type"]);
                                decimal amount = Convert.ToDecimal(reader["amount"]);
                                DateTime date = Convert.ToDateTime(reader["transaction_date"]);
                                string desc = Convert.ToString(reader["Description"]);

                                Console.WriteLine($"TxnID: {txnId} | Type: {type} | Amount: ₹{amount} | Date: {date} | Desc: {desc}");
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error while fetching transactions: " + ex.Message);
                }
            }
        }


        public void CalculateInterest()
        {
            string selectQuery = "SELECT account_id, balance, InterestRate FROM accounts WHERE account_type = 'savings'";
            string updateQuery = "UPDATE accounts SET balance = balance + @interestAmount WHERE account_id = @accNo";
            string insertTransactionQuery = "INSERT INTO transactions (account_id, transaction_type, amount, transaction_date, Description) " +
                                            "VALUES (@accNo, 'Interest', @interestAmount, @date, @desc)";

            using (SqlConnection conn = DBUtil.GetConnection())
            {
                conn.Open();
                SqlTransaction transaction = conn.BeginTransaction();

                try
                {
                    using (SqlCommand selectCmd = new SqlCommand(selectQuery, conn, transaction))
                    {
                        using (SqlDataReader reader = selectCmd.ExecuteReader())
                        {
                            List<(long accId, decimal interestAmount)> interestEntries = new();

                            while (reader.Read())
                            {
                                long accId = Convert.ToInt64(reader["account_id"]);
                                decimal balance = Convert.ToDecimal(reader["balance"]);
                                decimal interestRate = Convert.ToDecimal(reader["InterestRate"]);

                                decimal interestAmount = balance * interestRate;

                                interestEntries.Add((accId, interestAmount));
                            }

                            reader.Close();

                            foreach (var (accId, interestAmount) in interestEntries)
                            {
                                using (SqlCommand updateCmd = new SqlCommand(updateQuery, conn, transaction))
                                {
                                    updateCmd.Parameters.AddWithValue("@interestAmount", interestAmount);
                                    updateCmd.Parameters.AddWithValue("@accNo", accId);
                                    updateCmd.ExecuteNonQuery();
                                }

                                using (SqlCommand insertTxnCmd = new SqlCommand(insertTransactionQuery, conn, transaction))
                                {
                                    insertTxnCmd.Parameters.AddWithValue("@accNo", accId);
                                    insertTxnCmd.Parameters.AddWithValue("@interestAmount", interestAmount);
                                    insertTxnCmd.Parameters.AddWithValue("@date", DateTime.Now);
                                    insertTxnCmd.Parameters.AddWithValue("@desc", "Interest credited");
                                    insertTxnCmd.ExecuteNonQuery();
                                }
                            }
                        }
                    }

                    transaction.Commit();
                    Console.WriteLine("Interest calculated and credited successfully to all savings accounts.");
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    Console.WriteLine("Error during interest calculation: " + ex.Message);
                }
            }
        }

        public decimal GetBalance(int accountId)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT balance FROM accounts WHERE account_id = @accountId";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@accountId", accountId);
                conn.Open();

                object result = cmd.ExecuteScalar();
                if (result == null)
                    throw new InvalidAccountException($"Account ID {accountId} not found.");

                return Convert.ToDecimal(result);
            }
        }

        // ✅ NEW METHOD: TransferFunds
        public void TransferFunds(int fromAccountId, int toAccountId, decimal amount)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlTransaction transaction = conn.BeginTransaction();

                try
                {
                    // Check fromAccount exists and has sufficient funds
                    decimal fromBalance = 0;
                    string accountType = "";
                    decimal overdraftLimit = 0;

                    string checkFrom = "SELECT balance, account_type, OverdraftLimit FROM accounts WHERE account_id = @fromId";
                    SqlCommand cmdFrom = new SqlCommand(checkFrom, conn, transaction);
                    cmdFrom.Parameters.AddWithValue("@fromId", fromAccountId);
                    using (SqlDataReader reader = cmdFrom.ExecuteReader())
                    {
                        if (!reader.Read())
                            throw new InvalidAccountException($"From Account ID {fromAccountId} not found.");

                        fromBalance = reader.GetDecimal(0);
                        accountType = reader.GetString(1);
                        overdraftLimit = reader.IsDBNull(2) ? 0 : reader.GetDecimal(2);
                    }

                    decimal effectiveLimit = accountType == "Current" ? overdraftLimit : 0;

                    if (fromBalance - amount < -effectiveLimit)
                        throw new InsufficientFundException("Not enough balance for transfer.");

                    // Check if toAccount exists
                    string checkTo = "SELECT COUNT(*) FROM accounts WHERE account_id = @toId";
                    SqlCommand cmdTo = new SqlCommand(checkTo, conn, transaction);
                    cmdTo.Parameters.AddWithValue("@toId", toAccountId);
                    int toCount = (int)cmdTo.ExecuteScalar();

                    if (toCount == 0)
                        throw new InvalidAccountException($"To Account ID {toAccountId} not found.");

                    // Withdraw from sender
                    string withdrawQuery = "UPDATE accounts SET balance = balance - @amount WHERE account_id = @fromId";
                    SqlCommand withdrawCmd = new SqlCommand(withdrawQuery, conn, transaction);
                    withdrawCmd.Parameters.AddWithValue("@amount", amount);
                    withdrawCmd.Parameters.AddWithValue("@fromId", fromAccountId);
                    withdrawCmd.ExecuteNonQuery();

                    // Deposit to receiver
                    string depositQuery = "UPDATE accounts SET balance = balance + @amount WHERE account_id = @toId";
                    SqlCommand depositCmd = new SqlCommand(depositQuery, conn, transaction);
                    depositCmd.Parameters.AddWithValue("@amount", amount);
                    depositCmd.Parameters.AddWithValue("@toId", toAccountId);
                    depositCmd.ExecuteNonQuery();

                    // Insert transactions
                    string insertTxn = @"INSERT INTO transactions (account_id, transaction_type, amount, transaction_date, description)
                                     VALUES (@accId, @type, @amount, @date, @desc)";

                    SqlCommand fromTxn = new SqlCommand(insertTxn, conn, transaction);
                    fromTxn.Parameters.AddWithValue("@accId", fromAccountId);
                    fromTxn.Parameters.AddWithValue("@type", "Transfer-Out");
                    fromTxn.Parameters.AddWithValue("@amount", amount);
                    fromTxn.Parameters.AddWithValue("@date", DateTime.Now);
                    fromTxn.Parameters.AddWithValue("@desc", $"Transferred to {toAccountId}");
                    fromTxn.ExecuteNonQuery();

                    SqlCommand toTxn = new SqlCommand(insertTxn, conn, transaction);
                    toTxn.Parameters.AddWithValue("@accId", toAccountId);
                    toTxn.Parameters.AddWithValue("@type", "Transfer-In");
                    toTxn.Parameters.AddWithValue("@amount", amount);
                    toTxn.Parameters.AddWithValue("@date", DateTime.Now);
                    toTxn.Parameters.AddWithValue("@desc", $"Received from {fromAccountId}");
                    toTxn.ExecuteNonQuery();

                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }


        }
        

    }
}
