using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingSystem.DatabaseConnectivity.Bean
{
    public class CurrentAccount : Account
    {
        // Overdraft limit for current account
        public decimal OverdraftLimit { get; set; }

        // Constructor using base class constructor
        public CurrentAccount(decimal balance, Customer customer, decimal overdraftLimit)
            : base("Current", balance, customer)
        {
            this.OverdraftLimit = overdraftLimit;
        }

        // Parameterless constructor
        public CurrentAccount() { }
    }
}
