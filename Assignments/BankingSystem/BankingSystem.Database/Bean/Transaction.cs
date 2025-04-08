using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingSystem.DatabaseConnectivity.Bean
{
    public class Transaction
    {
        // Properties
        public long AccountNumber { get; set; }
        public string Description { get; set; }
        public DateTime TransactionDateTime { get; set; }
        public string TransactionType { get; set; } // e.g., "Deposit", "Withdraw", "Transfer"
        public decimal TransactionAmount { get; set; }

        // Default constructor
        public Transaction() { }

        // Parameterized constructor with corrected decimal type
        public Transaction(long accountNumber, string description, DateTime transactionDateTime, string transactionType, decimal amount)
        {
            AccountNumber = accountNumber;
            Description = description;
            TransactionDateTime = transactionDateTime;
            TransactionType = transactionType;
            TransactionAmount = amount;
        }

        // Override ToString() for easy display
        public override string ToString()
        {
            return $"[{TransactionDateTime}] {TransactionType} of {TransactionAmount} for Acc No {AccountNumber} - {Description}";
        }
    }
}
