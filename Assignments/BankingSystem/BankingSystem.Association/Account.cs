using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingSystem.Association
{
    public class Account
    {
        private static long nextAccountNumber = 1001; // Auto-incrementing account number

        public long AccountNumber { get; private set; }
        public string AccountType { get; set; }
        public float AccountBalance { get; private set; }
        public Customer AccountHolder { get; set; } // Has-A relationship

        // Default constructor
        public Account()
        {
            AccountNumber = nextAccountNumber++;
        }

        // Parameterized constructor
        public Account(Customer customer, string accountType, float balance)
        {
            AccountNumber = nextAccountNumber++;
            AccountHolder = customer;
            AccountType = accountType;
            AccountBalance = balance;
        }

        // Deposit method
        public void Deposit(float amount)
        {
            if (amount > 0)
            {
                AccountBalance += amount;
                Console.WriteLine($"Deposited ₹{amount:N2}. New Balance: ₹{AccountBalance:N2}");
            }
            else
            {
                Console.WriteLine("Invalid deposit amount.");
            }
        }

        // Withdraw method
        public void Withdraw(float amount)
        {
            if (amount <= AccountBalance)
            {
                AccountBalance -= amount;
                Console.WriteLine($"Withdrawn ₹{amount:N2}. New Balance: ₹{AccountBalance:N2}");
            }
            else
            {
                Console.WriteLine("Insufficient balance.");
            }
        }

        // Get current balance
        public float GetBalance()
        {
            return AccountBalance;
        }

        // Display account + customer details
        public void DisplayAccountInfo()
        {
            Console.WriteLine("===== Account Information =====");
            Console.WriteLine($"Account No : {AccountNumber}");
            Console.WriteLine($"Type       : {AccountType}");
            Console.WriteLine($"Balance    : ₹{AccountBalance:N2}");
            Console.WriteLine();
            AccountHolder.DisplayCustomerInfo();
            Console.WriteLine("================================");
        }
    }
}
