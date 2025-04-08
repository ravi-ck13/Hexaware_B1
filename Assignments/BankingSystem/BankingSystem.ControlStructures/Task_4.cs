using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingSystem.ControlStructures
{
    // ------------------------- Task 4: Looping, Array and Data Validation -------------------------
    /*
        Task 4: Bank Account Balance Checker

        You are tasked with creating a program that allows bank customers to check their account balances.
        The program should handle multiple customer accounts, and the customer should be able to enter their
        account number and retrieve their balance.

        Steps:
        1. Use a loop (e.g., while loop) to repeatedly ask the user for their account number until they enter a valid account number.
        2. Validate the account number entered by checking it against the database.
        3. If the account number is valid, display the account balance.
        4. If not, ask the user to try again.
    */
    public class Task_4
    {
        private static string connectionString = "Server=RAVI\\SQLEXPRESS;Database=HMBank;Integrated Security=True;TrustServerCertificate=True;";

        public static void AccountBalanceChecker()
        {
            string accountId;
            decimal balance;

            Console.WriteLine("\n--- Bank Account Balance Checker ---");

            // Keep asking for a valid account number
            while (true)
            {
                Console.Write("\nEnter your Account ID: ");
                accountId = Console.ReadLine();

                // Check if account exists and fetch balance
                balance = GetBalance(accountId);

                if (balance != -1)
                {
                    Console.WriteLine($"\n Account Found! Your Balance: ${balance}");
                    break;
                }
                else
                {
                    Console.WriteLine(" Invalid Account ID! Please try again.");
                }
            }
            Console.WriteLine("\nPress any key to return to the main menu...");
            Console.ReadKey();
            Console.WriteLine("\nPress any key to return to the main menu...");
            Console.ReadKey();
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
    }
}
