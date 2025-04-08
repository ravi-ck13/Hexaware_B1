using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingSystem.OOP
{
    public class CurrentAccount : Account
    {
        private const double OverdraftLimit = 5000; // Define overdraft limit

        // Constructor with Correct Parameters
        public CurrentAccount(int accountNumber, double balance)
            : base(accountNumber, "Current", balance) // Passing "Current" as account type
        {
        }

        // Override Withdraw Method for Overdraft
        public override void Withdraw(double amount)
        {
            if (Balance - amount >= -OverdraftLimit)
            {
                Balance -= amount;
                Console.WriteLine($"Withdrawn: {amount}. New Balance: {Balance}");
            }
            else
            {
                Console.WriteLine($"Insufficient balance! Overdraft limit reached.");
            }
        }
    }
}
