using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankingSystem.DatabaseConnectivity.Bean;

namespace BankingSystem.DatabaseConnectivity.RepositoryImpl
{
    public interface IBankRepository
    {
        // Account related
        void CreateAccount(Customer customer, long accNo, string accType, decimal balance);
        List<Account> ListAccounts();
        decimal GetAccountBalance(long accountNumber);
        Account GetAccountDetails(long accountNumber);
        void CalculateInterest();

        // Transaction related
        decimal Deposit(long accountNumber, decimal amount);
        decimal Withdraw(long accountNumber, decimal amount, string description);
        void Transfer(long fromAccountNumber, long toAccountNumber, decimal amount);
        List<Transaction> GetTransactions(long accountNumber, DateTime fromDate, DateTime toDate);
    }
}
