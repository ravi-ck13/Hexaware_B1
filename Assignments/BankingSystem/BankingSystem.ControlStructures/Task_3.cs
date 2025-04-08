using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingSystem.ControlStructures
{
    public class Task_3
    {
        // Database connection string
        private static string connectionString = "Server=RAVI\\SQLEXPRESS;Database=HMBank;Integrated Security=True;TrustServerCertificate=True;";

        public static void CalculateFutureBalance()
        {
            Console.WriteLine("\nFuture Balance Calculator (Savings Accounts Only)");

            Console.Write("Enter the number of savings account customers to process: ");
            int customerCount;
            while (!int.TryParse(Console.ReadLine(), out customerCount) || customerCount <= 0)
            {
                Console.Write(" Invalid input! Enter a valid number of customers: ");
            }

            for (int i = 1; i <= customerCount; i++)
            {
                Console.WriteLine($"\n Customer {i}:");

                // Get Account ID
                Console.Write("Enter Account ID: ");
                string accountId = Console.ReadLine();

                // Fetch balance for Savings Account
                decimal initialBalance = GetSavingsBalance(accountId);
                if (initialBalance == -1)
                {
                    Console.WriteLine(" Account not found or is not a Savings Account!");
                    continue;
                }

                Console.WriteLine($" Initial Balance: ${initialBalance}");

                // Get user input for annual interest rate
                Console.Write("Enter Annual Interest Rate (%): ");
                double annualInterestRate;
                while (!double.TryParse(Console.ReadLine(), out annualInterestRate) || annualInterestRate <= 0)
                {
                    Console.Write(" Invalid interest rate! Enter a positive value: ");
                }

                // Get user input for number of years
                Console.Write("Enter Number of Years: ");
                int years;
                while (!int.TryParse(Console.ReadLine(), out years) || years <= 0)
                {
                    Console.Write(" Invalid number of years! Enter a positive value: ");
                }

                // Calculate future balance using compound interest formula
                double futureBalance = (double)initialBalance * Math.Pow((1 + annualInterestRate / 100), years);


                // Display the calculated balance
                Console.WriteLine($" Future Balance after {years} years: ${futureBalance:F2}");
            }

            // Pause before returning to menu
            Console.WriteLine("\nPress any key to return to the main menu...");
            Console.ReadKey();
            Console.WriteLine("\nPress any key to return to the main menu...");
            Console.ReadKey();
        }

        // Fetch balance from database only if account is a Savings account
        private static decimal GetSavingsBalance(string accountId)
        {
            decimal balance = -1;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT balance FROM Accounts WHERE account_id = @accountId AND account_type = 'Savings'";

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
