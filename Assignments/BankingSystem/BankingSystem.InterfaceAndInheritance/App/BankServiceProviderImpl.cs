using BankingSystem.InterfaceAndInheritance.Bean;
using BankingSystem.InterfaceAndInheritance.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingSystem.InterfaceAndInheritance.App
{
    public class BankServiceProviderImpl : IBankServiceProvider, ICustomerServiceProvider
    {
        //  Changed from List to HashSet to prevent duplicate accounts
        private HashSet<Account> accountList = new HashSet<Account>();

        public string branchName;
        public string branchAddress;

        public BankServiceProviderImpl(string branchName, string branchAddress)
        {
            this.branchName = branchName;
            this.branchAddress = branchAddress;
        }

        public void CreateAccount(Customer customer, string accType, float balance)
        {
            // Check for duplicate Aadhaar + AccountType before creating new account
            bool duplicateExists = accountList.Any(acc =>
                acc.Customer.AadhaarNumber == customer.AadhaarNumber &&
                acc.AccountType.Equals(accType, StringComparison.OrdinalIgnoreCase));

            if (duplicateExists)
            {
                Console.WriteLine(" Account already exists for this Aadhaar and account type. Duplicate not allowed.");
                return;
            }

            Account newAccount = null;

            if (accType.Equals("Savings", StringComparison.OrdinalIgnoreCase))
            {
                if (balance < 500)
                {
                    Console.WriteLine("Minimum balance for Savings Account is ₹500.");
                    return;
                }
                newAccount = new SavingsAccount(customer, balance);
            }
            else if (accType.Equals("Current", StringComparison.OrdinalIgnoreCase))
            {
                newAccount = new CurrentAccount(customer, balance);
            }
            else if (accType.Equals("ZeroBalance", StringComparison.OrdinalIgnoreCase))
            {
                newAccount = new ZeroBalanceAccount(customer);
            }
            else
            {
                Console.WriteLine("Invalid account type.");
                return;
            }

            accountList.Add(newAccount);
            Console.WriteLine($" Account created successfully! Account No: {newAccount.AccountNumber}");
        }


        public void CreateAccountInteractive()
        {
            Console.Write("Enter Customer ID: ");
            int id = int.Parse(Console.ReadLine());

            Console.Write("Enter Customer Name: ");
            string name = Console.ReadLine();

            Console.Write("Enter Email: ");
            string email = Console.ReadLine();

            Console.Write("Enter Phone: ");
            string phone = Console.ReadLine();

            Console.Write("Enter Address: ");
            string address = Console.ReadLine();

            Console.Write("Enter City: ");
            string city = Console.ReadLine();

            Console.Write("Enter Aadhaar Number: ");
            string aadhaar = Console.ReadLine();

            Customer customer = new Customer(id, name, email, phone, address, city, aadhaar);

            Console.WriteLine("Select Account Type:\n1. Savings\n2. Current\n3. ZeroBalance");
            string accChoice = Console.ReadLine();

            float initialBalance = 0;
            if (accChoice != "3")
            {
                Console.Write("Enter Initial Balance: ");
                initialBalance = float.Parse(Console.ReadLine());
            }

            string accType = accChoice switch
            {
                "1" => "Savings",
                "2" => "Current",
                "3" => "ZeroBalance",
                _ => "Savings"
            };

            CreateAccount(customer, accType, initialBalance);
        }

        public float Deposit(long accountNumber, float amount)
        {
            var account = FindAccount(accountNumber);
            account.Deposit(amount);
            return account.GetBalance();
        }

        public float Withdraw(long accountNumber, float amount)
        {
            var account = FindAccount(accountNumber);
            account.Withdraw(amount);
            return account.GetBalance();
        }

        public float GetAccountBalance(long accountNumber)
        {
            var account = FindAccount(accountNumber);
            return account.GetBalance();
        }

        public void GetAccountDetails(long accountNumber)
        {
            var account = FindAccount(accountNumber);
            account.DisplayAccountInfo();
        }

        public void Transfer(long fromAccount, long toAccount, float amount)
        {
            var sender = FindAccount(fromAccount);
            var receiver = FindAccount(toAccount);

            if (sender is SavingsAccount && sender.Balance - amount < 500)
                throw new InsufficientFundException("Savings account must maintain ₹500 minimum balance.");

            if (sender is CurrentAccount current && sender.Balance - amount < -current.OverdraftLimit)
                throw new OverDraftLimitExceededException("Current account overdraft limit exceeded.");

            if (!(sender is CurrentAccount) && sender.Balance < amount)
                throw new InsufficientFundException("Insufficient balance.");

            sender.Withdraw(amount);
            receiver.Deposit(amount);
            Console.WriteLine($" ₹{amount:N2} transferred from Acc No {fromAccount} to {toAccount}.");
        }

        public void ListAccounts()
        {
            Console.WriteLine("\n Sorted List of All Accounts by Customer Name:\n");

            var sortedList = accountList.ToList();
            sortedList.Sort(new CustomerNameComparer());

            foreach (var acc in sortedList)
            {
                acc.DisplayAccountInfo();
                Console.WriteLine("-----------------------------");
            }
        }


        public void CalculateInterest()
        {
            Console.WriteLine("\n Calculating interest for Savings Accounts...\n");
            foreach (var acc in accountList.OfType<SavingsAccount>())
            {
                float interest = acc.AccountBalance * acc.InterestRate / 100;
                Console.WriteLine($"Account No: {acc.AccountNumber} | Interest: ₹{interest:N2}");
            }
        }

        public Account FindAccount(long accNo)
        {
            var acc = accountList.FirstOrDefault(a => a.AccountNumber == accNo);
            if (acc == null)
                throw new InvalidAccountException($"Account number {accNo} not found.");
            return acc;
        }
    }
}
