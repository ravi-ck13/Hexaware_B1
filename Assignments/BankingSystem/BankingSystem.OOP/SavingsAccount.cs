using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingSystem.OOP
{
    public class SavingsAccount : Account
    {
        private const double InterestRate = 4.5;

        // Constructor with Correct Parameters
        public SavingsAccount(int accountNumber, double balance)
            : base(accountNumber, "Savings", balance) // Passing "Savings" as account type
        {
        }

        // Override Interest Calculation
        public override void CalculateInterest()
        {
            double interest = (Balance * InterestRate) / 100;
            Console.WriteLine($"Interest Earned: {interest}");
            Deposit(interest);
        }
    }
}
