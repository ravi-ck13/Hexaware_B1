using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingSystem.Abstraction
{
    public abstract class BankAccount
    {
        public int AccountNumber { get; set; }
        public string CustomerName { get; set; }
        public double Balance { get; set; }

        // Default constructor
        public BankAccount() { }

        // Overloaded constructor
        public BankAccount(int accountNumber, string customerName, double balance)
        {
            AccountNumber = accountNumber;
            CustomerName = customerName;
            Balance = balance;
        }

        public void DisplayInfo()
        {
            Console.WriteLine($"Account Number: {AccountNumber}");
            Console.WriteLine($"Customer Name: {CustomerName}");
            Console.WriteLine($"Balance: {Balance:C}");
        }

        public abstract void Deposit(double amount);
        public abstract void Withdraw(double amount);
        public abstract void CalculateInterest();
    }
}
