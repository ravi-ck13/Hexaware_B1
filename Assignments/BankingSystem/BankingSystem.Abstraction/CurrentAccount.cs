using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingSystem.Abstraction
{
    public class CurrentAccount : BankAccount
    {
        private const double OverdraftLimit = 5000;

        public CurrentAccount() { }

        public CurrentAccount(int accountNumber, string customerName, double balance)
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
            if (Balance + OverdraftLimit >= amount)
            {
                Balance -= amount;
                Console.WriteLine($"Withdrawn {amount:C}. New Balance: {Balance:C}");
            }
            else
            {
                Console.WriteLine("Overdraft limit exceeded. Cannot withdraw.");
            }
        }

        public override void CalculateInterest()
        {
            Console.WriteLine("Current accounts do not earn interest.");
        }
    }
}
