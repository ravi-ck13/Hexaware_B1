using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingSystem.InterfaceAndInheritance.Bean
{
    public class CurrentAccount : Account
    {
        public float OverdraftLimit { get; set; }

        public CurrentAccount(Customer customer, float balance, float overdraftLimit = 2000)
            : base(customer, "Current", balance)
        {
            OverdraftLimit = overdraftLimit;
        }

        public override void Withdraw(float amount)
        {
            if (Balance + OverdraftLimit >= amount)
            {
                Balance -= amount;
                Console.WriteLine($"Withdrew ₹{amount:N2}. New Balance: ₹{Balance:N2}");
            }
            else
            {
                throw new OverDraftLimitExceededException("Withdrawal exceeds overdraft limit.");
            }
        }


    }
}
