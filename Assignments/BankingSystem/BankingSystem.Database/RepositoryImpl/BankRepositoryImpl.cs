using BankingSystem.DatabaseConnectivity.Util;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankingSystem.DatabaseConnectivity.Bean;

namespace BankingSystem.DatabaseConnectivity.RepositoryImpl
{
    public class BankRepositoryImpl : IBankRepository
    {
        public void CreateAccount(Customer customer, long accNo, string accType, decimal balance)
        {
            string insertQuery = "INSERT INTO accounts (account_id, customer_id, account_type, balance, InterestRate, OverdraftLimit) " +
                                 "VALUES (@AccountId, @CustomerId, @AccountType, @Balance, @InterestRate, @OverdraftLimit)";

            using (SqlConnection conn = DBUtil.GetConnection())
            {
                SqlCommand cmd = new SqlCommand(insertQuery, conn);
                cmd.Parameters.AddWithValue("@AccountId", accNo);
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
                else // ZeroBalance
                {
                    cmd.Parameters.AddWithValue("@InterestRate", DBNull.Value);
                    cmd.Parameters.AddWithValue("@OverdraftLimit", DBNull.Value);
                }

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public List<Account> ListAccounts()
        {
            List<Account> accounts = new List<Account>();
            string query = "SELECT * FROM accounts";

            using (SqlConnection conn = DBUtil.GetConnection())
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    string type = reader["account_type"].ToString().ToLower();
                    Account account = type switch
                    {
                        "savings" => new SavingsAccount(),
                        "current" => new CurrentAccount(),
                        _ => new ZeroBalanceAccount()
                    };

                    account.AccountNumber = (int)reader["account_id"];
                    account.AccountType = type;
                    account.Balance = Convert.ToDecimal(reader["balance"]);
                    accounts.Add(account);
                }
            }

            return accounts;
        }

        public decimal GetAccountBalance(long accountNumber)
        {
            using (SqlConnection conn = DBUtil.GetConnection())
            {
                string query = "SELECT balance FROM accounts WHERE account_id = @AccountId";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@AccountId", accountNumber);
                conn.Open();
                object result = cmd.ExecuteScalar();

                if (result != null && result != DBNull.Value)
                    return Convert.ToDecimal(result);

                throw new Exception("Account not found.");
            }
        }

        public decimal Deposit(long accountNumber, decimal amount)
        {
            decimal newBalance = GetAccountBalance(accountNumber) + amount;

            using (SqlConnection conn = DBUtil.GetConnection())
            {
                string updateQuery = "UPDATE accounts SET balance = @Balance WHERE account_id = @AccountId";
                SqlCommand cmd = new SqlCommand(updateQuery, conn);
                cmd.Parameters.AddWithValue("@Balance", newBalance);
                cmd.Parameters.AddWithValue("@AccountId", accountNumber);
                conn.Open();
                cmd.ExecuteNonQuery();

                InsertTransaction(accountNumber, "DEPOSIT", amount, "Amount deposited successfully", conn);
            }

            return newBalance;
        }

        public decimal Withdraw(long accountNumber, decimal amount, string description)
        {
            decimal currentBalance = GetAccountBalance(accountNumber);
            string accountType = "";

            using (SqlConnection conn = DBUtil.GetConnection())
            {
                string getTypeQuery = "SELECT account_type FROM accounts WHERE account_id = @AccountId";
                SqlCommand cmd = new SqlCommand(getTypeQuery, conn);
                cmd.Parameters.AddWithValue("@AccountId", accountNumber);
                conn.Open();
                object result = cmd.ExecuteScalar();
                accountType = result?.ToString()?.ToLower();
            }

            if (accountType == "savings" && currentBalance - amount < 500)
                throw new Exception("Savings account must maintain a minimum balance of 500.");
            else if (accountType == "current")
            {
                decimal overdraftLimit = 10000;
                if (currentBalance - amount < -overdraftLimit)
                    throw new Exception("Overdraft limit exceeded.");
            }
            else if (currentBalance < amount)
                throw new Exception("Insufficient funds.");

            decimal newBalance = currentBalance - amount;

            using (SqlConnection conn = DBUtil.GetConnection())
            {
                string updateQuery = "UPDATE accounts SET balance = @Balance WHERE account_id = @AccountId";
                SqlCommand cmd = new SqlCommand(updateQuery, conn);
                cmd.Parameters.AddWithValue("@Balance", newBalance);
                cmd.Parameters.AddWithValue("@AccountId", accountNumber);
                conn.Open();
                cmd.ExecuteNonQuery();

                InsertTransaction(accountNumber, "WITHDRAW", amount, description, conn);
            }

            return newBalance;
        }

        public void Transfer(long fromAccount, long toAccount, decimal amount)
        {
            using (SqlConnection conn = DBUtil.GetConnection())
            {
                conn.Open();
                SqlTransaction transaction = conn.BeginTransaction();

                try
                {
                    decimal fromBalance = GetAccountBalance(fromAccount);
                    string typeQuery = "SELECT account_type FROM accounts WHERE account_id = @AccountId";
                    SqlCommand typeCmd = new SqlCommand(typeQuery, conn, transaction);
                    typeCmd.Parameters.AddWithValue("@AccountId", fromAccount);
                    string accountType = typeCmd.ExecuteScalar()?.ToString()?.ToLower();

                    if (accountType == "savings" && fromBalance - amount < 500)
                        throw new Exception("Minimum balance requirement not met.");
                    else if (accountType == "current" && fromBalance - amount < -10000)
                        throw new Exception("Overdraft limit exceeded.");
                    else if (fromBalance < amount && accountType != "current")
                        throw new Exception("Insufficient funds.");

                    SqlCommand withdrawCmd = new SqlCommand("UPDATE accounts SET balance = balance - @Amount WHERE account_id = @From", conn, transaction);
                    withdrawCmd.Parameters.AddWithValue("@Amount", amount);
                    withdrawCmd.Parameters.AddWithValue("@From", fromAccount);
                    withdrawCmd.ExecuteNonQuery();

                    SqlCommand depositCmd = new SqlCommand("UPDATE accounts SET balance = balance + @Amount WHERE account_id = @To", conn, transaction);
                    depositCmd.Parameters.AddWithValue("@Amount", amount);
                    depositCmd.Parameters.AddWithValue("@To", toAccount);
                    depositCmd.ExecuteNonQuery();

                    InsertTransaction(fromAccount, "TRANSFER", amount, $"Transferred to {toAccount}", conn, transaction);
                    InsertTransaction(toAccount, "TRANSFER", amount, $"Received from {fromAccount}", conn, transaction);

                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public Account GetAccountDetails(long accountNumber)
        {
            string query = "SELECT * FROM accounts WHERE account_id = @AccountId";

            using (SqlConnection conn = DBUtil.GetConnection())
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@AccountId", accountNumber);
                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    string type = reader["account_type"].ToString().ToLower();
                    Account account = type switch
                    {
                        "savings" => new SavingsAccount(),
                        "current" => new CurrentAccount(),
                        _ => new ZeroBalanceAccount()
                    };

                    account.AccountNumber = (int)reader["account_id"];
                    account.AccountType = type;
                    account.Balance = Convert.ToDecimal(reader["balance"]);
                    return account;
                }
            }

            return null;
        }

        public void CalculateInterest()
        {
            string selectQuery = "SELECT account_id, balance, InterestRate FROM accounts WHERE account_type = 'savings' AND InterestRate IS NOT NULL";

            using (SqlConnection conn = DBUtil.GetConnection())
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(selectQuery, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                List<(long, decimal, decimal)> updates = new List<(long, decimal, decimal)>();

                while (reader.Read())
                {
                    long accId = (long)reader["account_id"];
                    decimal balance = Convert.ToDecimal(reader["balance"]);
                    decimal rate = Convert.ToDecimal(reader["InterestRate"]);
                    decimal interest = balance * rate;
                    updates.Add((accId, balance + interest, interest));
                }

                reader.Close();

                foreach (var item in updates)
                {
                    SqlCommand updateCmd = new SqlCommand("UPDATE accounts SET balance = @Balance WHERE account_id = @AccountId", conn);
                    updateCmd.Parameters.AddWithValue("@Balance", item.Item2);
                    updateCmd.Parameters.AddWithValue("@AccountId", item.Item1);
                    updateCmd.ExecuteNonQuery();

                    InsertTransaction(item.Item1, "INTEREST", item.Item3, "Interest credited", conn);
                }
            }
        }

        public List<Transaction> GetTransactions(long accountNumber, DateTime fromDate, DateTime toDate)
        {
            List<Transaction> transactions = new List<Transaction>();
            string query = "SELECT * FROM transactions WHERE account_id = @AccountId AND transaction_date BETWEEN @From AND @To";

            using (SqlConnection conn = DBUtil.GetConnection())
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@AccountId", accountNumber);
                cmd.Parameters.AddWithValue("@From", fromDate);
                cmd.Parameters.AddWithValue("@To", toDate);
                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Transaction txn = new Transaction
                    {
                        AccountNumber = (long)reader["account_id"],
                        TransactionDateTime = Convert.ToDateTime(reader["transaction_date"]),
                        TransactionAmount = Convert.ToDecimal(reader["amount"]),
                        TransactionType = reader["transaction_type"].ToString(),
                        Description = reader["description"].ToString()
                    };

                    transactions.Add(txn);
                }
            }

            return transactions;
        }

        private void InsertTransaction(long accountNumber, string type, decimal amount, string description, SqlConnection conn, SqlTransaction transaction = null)
        {
            string insertQuery = "INSERT INTO transactions (account_id, transaction_type, amount, transaction_date, description) " +
                                 "VALUES (@AccountId, @Type, @Amount, @Date, @Description)";

            SqlCommand cmd = new SqlCommand(insertQuery, conn, transaction);
            cmd.Parameters.AddWithValue("@AccountId", accountNumber);
            cmd.Parameters.AddWithValue("@Type", type);
            cmd.Parameters.AddWithValue("@Amount", amount);
            cmd.Parameters.AddWithValue("@Date", DateTime.Now);
            cmd.Parameters.AddWithValue("@Description", description);

            cmd.ExecuteNonQuery();
        }
    }
}
