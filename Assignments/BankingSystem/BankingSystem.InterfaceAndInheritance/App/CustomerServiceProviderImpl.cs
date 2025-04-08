using BankingSystem.InterfaceAndInheritance.Bean;
using BankingSystem.InterfaceAndInheritance.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingSystem.InterfaceAndInheritance.App
{
    public class CustomerServiceProviderImpl : ICustomerServiceProvider
    {
        protected List<Account> accountList = new List<Account>();

        public float Deposit(long accountNumber, float amount)
        {
            var account = accountList.Find(a => a.AccountNumber == accountNumber);
            if (account != null)
            {
                account.Deposit(amount);
                return account.GetBalance();
            }
            Console.WriteLine("Account not found.");
            return -1;
        }

        public float GetAccountBalance(long accountNumber)
        {
            var account = accountList.Find(a => a.AccountNumber == accountNumber);
            if (account != null)
                return account.GetBalance();

            Console.WriteLine("Account not found.");
            return -1;
        }

        public void GetAccountDetails(long accountNumber)
        {
            var account = accountList.Find(a => a.AccountNumber == accountNumber);
            if (account != null)
                account.DisplayAccountInfo();
            else
                Console.WriteLine("Account not found.");
        }

        public float Withdraw(long accountNumber, float amount)
        {
            var account = accountList.Find(a => a.AccountNumber == accountNumber);
            if (account != null)
            {
                account.Withdraw(amount);
                return account.GetBalance();
            }
            Console.WriteLine("Account not found.");
            return -1;
        }

        public void Transfer(long fromAccount, long toAccount, float amount)
        {
            var sender = accountList.Find(a => a.AccountNumber == fromAccount);
            var receiver = accountList.Find(a => a.AccountNumber == toAccount);

            if (sender == null || receiver == null)
            {
                Console.WriteLine("One or both accounts not found.");
                return;
            }

            if (sender.GetBalance() >= amount)
            {
                sender.Withdraw(amount);
                receiver.Deposit(amount);
                Console.WriteLine($"₹{amount:N2} transferred successfully.");
            }
            else
            {
                Console.WriteLine("Insufficient funds for transfer.");
            }
        }
    }
}
