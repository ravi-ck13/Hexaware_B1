using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingSystem.Abstraction
{
    public class SavingsAccount : BankAccount
    {
        public double InterestRate { get; set; } = 4.5;

        public SavingsAccount() { }

        public SavingsAccount(int accountNumber, string customerName, double balance)
            : base(accountNumber, customerName, balance)
        {
        }

        public override void Deposit(double amount)
        {
            Balance += amount;
            Console.WriteLine($"Deposited {amount:C}. New Balance: {Balance:C}");
        }

        public override void Withdraw(double amount)
        {
            if (amount <= Balance)
            {
                Balance -= amount;
                Console.WriteLine($"Withdrawn {amount:C}. New Balance: {Balance:C}");
            }
            else
            {
                Console.WriteLine("Insufficient balance.");
            }
        }

        public override void CalculateInterest()
        {
            double interest = Balance * InterestRate / 100;
            Balance += interest;
            Console.WriteLine($"Interest of {interest:C} added. New Balance: {Balance:C}");
        }
    }
}
