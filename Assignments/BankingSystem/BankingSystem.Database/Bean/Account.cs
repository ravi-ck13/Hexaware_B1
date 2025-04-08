using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingSystem.DatabaseConnectivity.Bean
{
    public abstract class Account
    {
        // Static variable to generate unique account numbers
        private static int lastAccNo = 1000;

        // Properties
        public int AccountNumber { get; set; }
        public decimal Balance { get; set; } // Changed from float to decimal
        public string AccountType { get; set; }
        public Customer Customer { get; set; }

        // Default constructor
        public Account() { }

        // Parameterized constructor
        public Account(string accountType, decimal balance, Customer customer)
        {
            AccountNumber = ++lastAccNo; // Auto-increment account number
            AccountType = accountType;
            Balance = balance;
            Customer = customer;
        }

        // Override ToString() to display account details
        public override string ToString()
        {
            return $"Account No: {AccountNumber}, Type: {AccountType}, Balance: {Balance}, Customer: {Customer.FirstName} {Customer.LastName}";
        }
    }
}
