using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BankingSystem.OOP
{
    // Base class
    public class Account
    {
        protected int AccountNumber;
        protected string AccountType;
        protected double Balance;

        // Default Constructor
        public Account() { }

        // Parameterized Constructor
        public Account(int accountNumber, string accountType, double balance)
        {
            AccountNumber = accountNumber;
            AccountType = accountType;
            Balance = balance;
        }

        // Deposit Method (Overloaded)
        public void Deposit(float amount)
        {
            Balance += amount;
            Console.WriteLine($"Deposited {amount}. New balance: {Balance}");
        }

        public void Deposit(int amount)
        {
            Balance += amount;
            Console.WriteLine($"Deposited {amount}. New balance: {Balance}");
        }

        public void Deposit(double amount)
        {
            Balance += amount;
            Console.WriteLine($"Deposited {amount}. New balance: {Balance}");
        }

        // Withdraw Method (Overloaded)
        public virtual void Withdraw(float amount)
        {
            if (amount <= Balance)
            {
                Balance -= amount;
                Console.WriteLine($"Withdrawn {amount}. New balance: {Balance}");
            }
            else
            {
                Console.WriteLine("Insufficient Balance!");
            }
        }

        public virtual void Withdraw(int amount)
        {
            if (amount <= Balance)
            {
                Balance -= amount;
                Console.WriteLine($"Withdrawn {amount}. New balance: {Balance}");
            }
            else
            {
                Console.WriteLine("Insufficient Balance!");
            }
        }

        public virtual void Withdraw(double amount)
        {
            if (amount <= Balance)
            {
                Balance -= amount;
                Console.WriteLine($"Withdrawn {amount}. New balance: {Balance}");
            }
            else
            {
                Console.WriteLine("Insufficient Balance!");
            }
        }

        // Virtual Method for Interest Calculation
        public virtual void CalculateInterest()
        {
            Console.WriteLine("Interest calculation not applicable for this account type.");
        }

        // Print Account Details
        public void PrintDetails()
        {
            Console.WriteLine($"Account Number: {AccountNumber}");
            Console.WriteLine($"Account Type: {AccountType}");
            Console.WriteLine($"Balance: {Balance}");
        }
    }
}
