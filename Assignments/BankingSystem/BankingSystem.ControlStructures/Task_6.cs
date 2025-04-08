using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingSystem.ControlStructures
{
    // ------------------------- Task 6: Bank Transaction History -------------------------
    /*
        Task 6: Bank Transaction History

        Create a program that maintains a list of bank transactions (deposits and withdrawals) for a customer.
        1. Use a while loop to allow the user to keep adding transactions until they choose to exit.
        2. Store transactions in the "Transactions" table in the database.
        3. Update the "Accounts" table to reflect the new balance.
        4. Display the transaction history upon exit.

        Database Tables:
        - Accounts: (account_id, customer_id, account_type, balance)
        - Transactions: (transaction_id, account_id, transaction_type, amount, transaction_date)
    */
    public class Task_6
    {
        private static string connectionString = "Server=RAVI\\SQLEXPRESS;Database=HMBank;Integrated Security=True;TrustServerCertificate=True;";

        public static void ManageTransactions()
        {
            Console.Write("\nEnter your Account ID: ");
            string accountId = Console.ReadLine();

            decimal balance = GetBalance(accountId);
            if (balance == -1)
            {
                Console.WriteLine("Account not found! Please enter a valid account ID.");
                return;
            }

            while (true)
            {
                Console.WriteLine("\nChoose a transaction:\n1. Deposit\n2. Withdraw\n3. Exit & View Transactions");
                Console.Write("Enter your choice: ");

                int choice;
                while (!int.TryParse(Console.ReadLine(), out choice) || choice < 1 || choice > 3)
                {
                    Console.Write("Invalid choice! Please enter 1, 2, or 3: ");
                }

                if (choice == 3)
                {
                    ShowTransactionHistory(accountId);
                    break; // Exit loop
                }

                Console.Write("Enter amount: ");
                decimal amount;
                while (!decimal.TryParse(Console.ReadLine(), out amount) || amount <= 0)
                {
                    Console.Write("Invalid amount! Please enter a positive value: ");
                }

                if (choice == 2 && amount > balance)
                {
                    Console.WriteLine(" Insufficient balance!");
                    continue; // Skip withdrawal if not enough funds
                }

                string transactionType = (choice == 1) ? "Deposit" : "Withdrawal";
                ProcessTransaction(accountId, transactionType, amount);
                balance = GetBalance(accountId); // Update balance after transaction
                Console.WriteLine($" {transactionType} successful! New balance: ${balance}");
            }
        }

        // Fetch balance from the database
        private static decimal GetBalance(string accountId)
        {
            decimal balance = -1;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT balance FROM Accounts WHERE account_id = @accountId";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@accountId", accountId);
                    object result = cmd.ExecuteScalar();
                    if (result != null)
                    {
                        balance = Convert.ToDecimal(result);
                    }
                }
            }
            return balance;
        }

        // Generate the next available transaction_id
        private static int GetNextTransactionId()
        {
            int nextId = 1;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT ISNULL(MAX(transaction_id), 0) + 1 FROM Transactions";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    nextId = Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
            return nextId;
        }

        // Process a deposit or withdrawal transaction
        private static void ProcessTransaction(string accountId, string transactionType, decimal amount)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                // Get next transaction_id
                int transactionId = GetNextTransactionId();

                // Insert the transaction record
                string insertQuery = "INSERT INTO Transactions (transaction_id, account_id, transaction_type, amount, transaction_date) " +
                                     "VALUES (@transactionId, @accountId, @transactionType, @amount, GETDATE())";
                using (SqlCommand cmd = new SqlCommand(insertQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@transactionId", transactionId);
                    cmd.Parameters.AddWithValue("@accountId", accountId);
                    cmd.Parameters.AddWithValue("@transactionType", transactionType);
                    cmd.Parameters.AddWithValue("@amount", amount);
                    cmd.ExecuteNonQuery();
                }

                // Update balance in the Accounts table
                string updateQuery = (transactionType == "Deposit") ?
                    "UPDATE Accounts SET balance = balance + @amount WHERE account_id = @accountId" :
                    "UPDATE Accounts SET balance = balance - @amount WHERE account_id = @accountId";

                using (SqlCommand cmd = new SqlCommand(updateQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@amount", amount);
                    cmd.Parameters.AddWithValue("@accountId", accountId);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        // Display transaction history for the user
        private static void ShowTransactionHistory(string accountId)
        {
            Console.WriteLine("\n Transaction History:");
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT transaction_id, transaction_type, amount, transaction_date FROM Transactions WHERE account_id = @accountId";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@accountId", accountId);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (!reader.HasRows)
                        {
                            Console.WriteLine("No transactions found.");
                        }
                        else
                        {
                            while (reader.Read())
                            {
                                Console.WriteLine($"ID: {reader["transaction_id"]} | Type: {reader["transaction_type"]} | Amount: ${reader["amount"]} | Date: {reader["transaction_date"]}");
                            }
                        }
                    }
                }
            }
        
            Console.WriteLine("\nPress any key to return to the main menu...");
            Console.ReadKey();
            Console.WriteLine("\nPress any key to return to the main menu...");
            Console.ReadKey();
        }
    }
}
