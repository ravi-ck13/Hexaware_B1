using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingSystem.InterfaceAndInheritance.Bean
{
    public class ZeroBalanceAccount : Account
    {
        public ZeroBalanceAccount(Customer customer)
            : base(customer, "ZeroBalance", 0)
        {
        }

        public override void Withdraw(float amount)
        {
            if (Balance >= amount)
            {
                base.Withdraw(amount);
            }
            else
            {
                Console.WriteLine("Insufficient balance. Zero balance accounts can't be overdrawn.");
            }
        }
    }
}
