using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingSystem.OOP
{
    public class Bank
    {
        public void PerformBankOperations()
        {
            Console.WriteLine("\nWelcome to the Banking System!");

            // Select Account Type
            Console.WriteLine("Select Account Type:");
            Console.WriteLine("1. Savings Account");
            Console.WriteLine("2. Current Account");
            Console.Write("Enter choice (1 or 2): ");
            int choice = int.Parse(Console.ReadLine());

            Account account = null;

            Console.Write("Enter Account Number: ");
            int accNum = int.Parse(Console.ReadLine());
            Console.Write("Enter Initial Balance: ");
            double balance = double.Parse(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    account = new SavingsAccount(accNum, balance);
                    break;

                case 2:
                    account = new CurrentAccount(accNum, balance);
                    break;

                default:
                    Console.WriteLine("Invalid choice! Exiting...");
                    return;
            }

            // Menu-driven system
            bool exit = false;
            while (!exit)
            {
                Console.WriteLine("\nSelect an operation:");
                Console.WriteLine("1. Deposit");
                Console.WriteLine("2. Withdraw");
                Console.WriteLine("3. Calculate Interest (Only for Savings)");
                Console.WriteLine("4. Display Account Details");
                Console.WriteLine("5. Exit");
                Console.Write("Enter your choice: ");
                int option = int.Parse(Console.ReadLine());

                switch (option)
                {
                    case 1:
                        Console.Write("Enter deposit amount: ");
                        double depositAmount = double.Parse(Console.ReadLine());
                        account.Deposit(depositAmount);
                        break;

                    case 2:
                        Console.Write("Enter withdrawal amount: ");
                        double withdrawAmount = double.Parse(Console.ReadLine());
                        account.Withdraw(withdrawAmount);
                        break;

                    case 3:
                        if (account is SavingsAccount savingsAccount)
                        {
                            savingsAccount.CalculateInterest();
                        }
                        else
                        {
                            Console.WriteLine("Interest calculation is only available for Savings Accounts.");
                        }
                        break;

                    case 4:
                        account.PrintDetails();
                        break;

                    case 5:
                        exit = true;
                        Console.WriteLine("Thank you for using the Banking System!");
                        break;

                    default:
                        Console.WriteLine("Invalid option! Please try again.");
                        break;
                }
            }
        }
    }
}
