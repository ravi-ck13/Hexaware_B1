using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace BankingSystem.ControlStructures
{
    // ------------------------- Task 2: Nested Conditional Statements -------------------------
    /*
        Task 2: Nested Conditional Statements

        Create a program that simulates an ATM transaction. 
        Display options such as:
            • "Check Balance"
            • "Withdraw"
            • "Deposit"

        Ask the user to enter their current balance and the amount they want to withdraw or deposit. 
        Implement checks to ensure that:
            • The withdrawal amount is not greater than the available balance
            • The withdrawal amount is in multiples of 100 or 500
        Display appropriate messages for success or failure.
    */

    public class Task_2
    {
        // Connection string (reuse this for database operations)
        private static string connectionString = "Server=RAVI\\SQLEXPRESS;Database=HMBank;Integrated Security=True;TrustServerCertificate=True;";

        public static void ATMTransaction()
        {
            Console.Write("\nEnter your Account ID: ");
            string accountId = Console.ReadLine();

            // Fetch balance from database
            decimal balance = GetBalance(accountId);
            if (balance == -1)
            {
                Console.WriteLine("Account not found! Please enter a valid account ID.");
                return;
            }

            Console.WriteLine($"\nYour Current Balance: ${balance}");
            Console.WriteLine("Choose an option:\n1. Check Balance\n2. Withdraw\n3. Deposit");
            Console.Write("Enter your choice: ");

            int choice;
            while (!int.TryParse(Console.ReadLine(), out choice) || choice < 1 || choice > 3)
            {
                Console.Write("Invalid choice! Please enter 1, 2, or 3: ");
            }

            switch (choice)
            {
                case 1:
                    Console.WriteLine($" Your balance is: ${balance}");
                    Console.WriteLine("\nPress Enter to return to the main menu...");
                    Console.ReadLine();
                    break;

                case 2:
                    Withdraw(accountId, balance);
                    break;

                case 3:
                    Deposit(accountId);
                    break;
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

        // Handle withdrawals
        private static void Withdraw(string accountId, decimal currentBalance)
        {
            Console.Write("Enter withdrawal amount (Multiples of 100 or 500): ");
            decimal amount;
            while (!decimal.TryParse(Console.ReadLine(), out amount) || (amount % 100 != 0 && amount % 500 != 0))
            {
                Console.Write("Invalid amount! Enter a valid amount (100 or 500 multiples): ");
            }

            if (amount > currentBalance)
            {
                Console.WriteLine("Insufficient balance!");
                return;
            }

            // Update balance in database
            UpdateBalance(accountId, currentBalance - amount);
            Console.WriteLine($"Withdrawal successful! New balance: ${currentBalance - amount}");
            Console.WriteLine("\nPress Enter to return to the main menu...");
            Console.ReadLine();
        }

        // Handle deposits
        private static void Deposit(string accountId)
        {
            Console.Write("Enter deposit amount: ");
            decimal amount;
            while (!decimal.TryParse(Console.ReadLine(), out amount) || amount <= 0)
            {
                Console.Write("Invalid amount! Enter a positive amount: ");
            }

            decimal currentBalance = GetBalance(accountId);
            decimal newBalance = currentBalance + amount;

            // Update balance in database
            UpdateBalance(accountId, newBalance);
            Console.WriteLine($" Deposit successful! New balance: ${newBalance}");
            Console.WriteLine("\nPress Enter to return to the main menu...");
            Console.ReadLine();
        }

        // Update the account balance in database
        private static void UpdateBalance(string accountId, decimal newBalance)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "UPDATE Accounts SET balance = @newBalance WHERE account_id = @accountId";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@newBalance", newBalance);
                    cmd.Parameters.AddWithValue("@accountId", accountId);
                    cmd.ExecuteNonQuery();
                }
            }
        }
        
    }

}
