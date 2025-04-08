using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingSystem.InterfaceAndInheritance.Bean
{
    public class SavingsAccount : Account
    {
        public float InterestRate { get; set; }

        public SavingsAccount(Customer customer, float balance, float interestRate = 0.05f)
            : base(customer, "Savings", balance < 500 ? 500 : balance)
        {
            InterestRate = interestRate;
        }

        public override void Withdraw(float amount)
        {
            if (Balance - amount >= 500)
            {
                Balance -= amount;
                Console.WriteLine($"Withdrew ₹{amount:N2}. New Balance: ₹{Balance:N2}");
            }
            else
            {
                throw new InsufficientFundException("Cannot withdraw. Minimum balance of ₹500 required.");
            }
        }


    }
}
