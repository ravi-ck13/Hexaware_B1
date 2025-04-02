using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UDException
{
    public class BankAccount
    {
        public int AccountNumber { get; }
        public double Balance { get; private set; }

        public BankAccount(int accountNumber, double initialBalance)
        {
            AccountNumber = accountNumber;
            Balance = initialBalance;
        }

        public void TransferFunds(double amount)
        {
            if (amount > Balance)
            {
                throw new InsufficientFundsException($"Insufficient funds! Your balance is {Balance}.", Balance);
            }

            Balance -= amount;
            Console.WriteLine($"Transfer successful! Remaining balance: {Balance}");
        }
    }
}
