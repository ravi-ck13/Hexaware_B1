using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace BankingSystem.Association
{
    public class Bank
    {
        private Dictionary<long, Account> accounts = new Dictionary<long, Account>();

        // Create Account via user input
        public void CreateAccount()
        {
            Console.Write("Enter First Name: ");
            string firstName = Console.ReadLine();

            Console.Write("Enter Last Name: ");
            string lastName = Console.ReadLine();

            string email;
            do
            {
                Console.Write("Enter Email Address: ");
                email = Console.ReadLine();
            } while (!Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"));

            string phone;
            do
            {
                Console.Write("Enter 10-digit Phone Number: ");
                phone = Console.ReadLine();
            } while (!Regex.IsMatch(phone, @"^\d{10}$"));

            Console.Write("Enter Address: ");
            string address = Console.ReadLine();

            Console.Write("Enter Account Type (Savings/Current): ");
            string accType = Console.ReadLine();

            Console.Write("Enter Initial Balance: ");
            float balance = float.Parse(Console.ReadLine());

            Customer customer = new Customer(firstName, lastName, email, phone, address);
            Account account = new Account(customer, accType, balance);
            accounts[account.AccountNumber] = account;

            Console.WriteLine($" Account created successfully. Account Number: {account.AccountNumber}");
        }

        public void Deposit()
        {
            Console.Write("Enter Account Number: ");
            long accNo = long.Parse(Console.ReadLine());

            Console.Write("Enter Amount to Deposit: ");
            float amount = float.Parse(Console.ReadLine());

            if (accounts.ContainsKey(accNo))
            {
                accounts[accNo].Deposit(amount);
                Console.WriteLine($" Deposited ₹{amount:N2}. New Balance: ₹{accounts[accNo].GetBalance():N2}");
            }
            else
            {
                Console.WriteLine(" Account not found.");
            }
        }

        public void Withdraw()
        {
            Console.Write("Enter Account Number: ");
            long accNo = long.Parse(Console.ReadLine());

            Console.Write("Enter Amount to Withdraw: ");
            float amount = float.Parse(Console.ReadLine());

            if (accounts.ContainsKey(accNo))
            {
                accounts[accNo].Withdraw(amount);
                Console.WriteLine($" Withdrawn ₹{amount:N2}. New Balance: ₹{accounts[accNo].GetBalance():N2}");
            }
            else
            {
                Console.WriteLine(" Account not found.");
            }
        }

        public void GetAccountBalance()
        {
            Console.Write("Enter Account Number: ");
            long accNo = long.Parse(Console.ReadLine());

            if (accounts.ContainsKey(accNo))
            {
                float balance = accounts[accNo].GetBalance();
                Console.WriteLine($" Current Balance: ₹{balance:N2}");
            }
            else
            {
                Console.WriteLine(" Account not found.");
            }
        }

        public void Transfer()
        {
            Console.Write("Enter Sender Account Number: ");
            long fromAcc = long.Parse(Console.ReadLine());

            Console.Write("Enter Receiver Account Number: ");
            long toAcc = long.Parse(Console.ReadLine());

            Console.Write("Enter Amount to Transfer: ");
            float amount = float.Parse(Console.ReadLine());

            if (!accounts.ContainsKey(fromAcc))
            {
                Console.WriteLine(" Sender account not found.");
                return;
            }

            if (!accounts.ContainsKey(toAcc))
            {
                Console.WriteLine(" Receiver account not found.");
                return;
            }

            if (accounts[fromAcc].GetBalance() < amount)
            {
                Console.WriteLine(" Insufficient funds to transfer.");
                return;
            }

            accounts[fromAcc].Withdraw(amount);
            accounts[toAcc].Deposit(amount);
            Console.WriteLine($" ₹{amount:N2} transferred from {fromAcc} to {toAcc}.");
        }

        public void GetAccountDetails()
        {
            Console.Write("Enter Account Number: ");
            long accNo = long.Parse(Console.ReadLine());

            if (accounts.ContainsKey(accNo))
            {
                accounts[accNo].DisplayAccountInfo();
            }
            else
            {
                Console.WriteLine(" Account not found.");
            }
        }
    }
}
