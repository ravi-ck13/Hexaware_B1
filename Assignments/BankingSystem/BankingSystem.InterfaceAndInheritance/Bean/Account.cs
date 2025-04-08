using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingSystem.InterfaceAndInheritance.Bean
{
    public class Account
    {
        // Unique identifier for the account
        public long AccountNumber { get; }

        public string AccountType { get; set; }

        //  Protected balance field used internally
        public float Balance { get; protected set; }

        //  Exposed public read-only property
        public float AccountBalance => Balance;

        public Customer Customer { get; set; }

        //  Static to auto-increment account numbers
        protected static long lastAccNo = 1000;

        //  Constructor
        public Account(Customer customer, string accType, float balance)
        {
            AccountNumber = ++lastAccNo;
            Customer = customer;
            AccountType = accType;
            Balance = balance;
        }

        //  Deposit funds
        public virtual void Deposit(float amount)
        {
            Balance += amount;
            Console.WriteLine($"Deposited ₹{amount:N2}. New Balance: ₹{Balance:N2}");
        }

        //  Withdraw funds with account type rules
        public virtual void Withdraw(float amount)
        {
            if (this is SavingsAccount)
            {
                if (Balance - amount < 500)
                    throw new InsufficientFundException("Savings Account requires a minimum balance of ₹500.");
            }
            else if (this is CurrentAccount currentAcc)
            {
                if (Balance - amount < -currentAcc.OverdraftLimit)
                    throw new OverDraftLimitExceededException("Current Account exceeded the overdraft limit.");
            }
            else if (Balance < amount)
            {
                throw new InsufficientFundException("Insufficient balance.");
            }

            Balance -= amount;
            Console.WriteLine($"Withdrew ₹{amount:N2}. New Balance: ₹{Balance:N2}");
        }

        //  Display all account and customer details
        public virtual void DisplayAccountInfo()
        {
            Console.WriteLine($"Account No: {AccountNumber}, Type: {AccountType}, Balance: ₹{Balance:N2}");
            Customer.DisplayCustomerInfo();
        }

        //  Simple balance getter
        public virtual float GetBalance()
        {
            return Balance;
        }

        //  Equals override to detect duplicate account by AadhaarNumber
        public override bool Equals(object obj)
        {
            if (obj is Account other)
            {
                return this.Customer?.AadhaarNumber == other.Customer?.AadhaarNumber;
            }
            return false;
        }

        //  Hash code based on AadhaarNumber
        public override int GetHashCode()
        {
            return this.Customer?.AadhaarNumber?.GetHashCode() ?? 0;
        }
    }
}
