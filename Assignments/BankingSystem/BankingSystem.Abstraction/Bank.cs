using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingSystem.Abstraction
{
    public class Bank
    {
        private BankAccount account;

        public void CreateAccount()
        {
            Console.WriteLine("\nChoose Account Type:");
            Console.WriteLine("1. Savings Account");
            Console.WriteLine("2. Current Account");
            Console.Write("Enter choice: ");
            int choice = int.Parse(Console.ReadLine());

            Console.Write("Enter Account Number: ");
            int accNo = int.Parse(Console.ReadLine());

            Console.Write("Enter Customer Name: ");
            string name = Console.ReadLine();

            Console.Write("Enter Initial Balance: ");
            double balance = double.Parse(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    account = new SavingsAccount(accNo, name, balance);
                    break;
                case 2:
                    account = new CurrentAccount(accNo, name, balance);
                    break;
                default:
                    Console.WriteLine("Invalid choice.");
                    break;
            }
        }

        public void PerformOperations()
        {
            if (account == null)
            {
                Console.WriteLine("No account exists. Please create one first.");
                return;
            }

            int option;
            do
            {
                Console.WriteLine("\n--- Banking Menu ---");
                Console.WriteLine("1. Deposit");
                Console.WriteLine("2. Withdraw");
                Console.WriteLine("3. Calculate Interest");
                Console.WriteLine("4. Show Account Info");
                Console.WriteLine("5. Exit");
                Console.Write("Choose an option: ");
                option = int.Parse(Console.ReadLine());

                switch (option)
                {
                    case 1:
                        Console.Write("Enter amount to deposit: ");
                        double depAmount = double.Parse(Console.ReadLine());
                        account.Deposit(depAmount);
                        break;

                    case 2:
                        Console.Write("Enter amount to withdraw: ");
                        double withAmount = double.Parse(Console.ReadLine());
                        account.Withdraw(withAmount);
                        break;

                    case 3:
                        account.CalculateInterest();
                        break;

                    case 4:
                        account.DisplayInfo();
                        break;

                    case 5:
                        Console.WriteLine("Exiting Banking Menu...");
                        break;

                    default:
                        Console.WriteLine("Invalid option.");
                        break;
                }
            } while (option != 5);
        }
    }
}
