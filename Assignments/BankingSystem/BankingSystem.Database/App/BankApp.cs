using BankingSystem.DatabaseConnectivity.Bean;
using BankingSystem.DatabaseConnectivity.ServiceImpl;
namespace BankingSystem.DatabaseConnectivity.App
{
    internal class BankApp
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            BankServiceProviderImpl bank = new BankServiceProviderImpl("Ravi's Branch", "Chennai");

            while (true)
            {
                Console.WriteLine("\n====== Welcome to HMBank ======");
                Console.WriteLine("1. Create Account");
                Console.WriteLine("2. Deposit");
                Console.WriteLine("3. Withdraw");
                Console.WriteLine("4. View Transaction History");
                Console.WriteLine("5. Get Balance");
                Console.WriteLine("6. Transfer Funds");
                Console.WriteLine("7. Get Account Details");
                Console.WriteLine("8. List All Accounts");
                Console.WriteLine("9. Exit");
                Console.Write("Choose an option: ");

                if (!int.TryParse(Console.ReadLine(), out int choice))
                {
                    Console.WriteLine("Invalid input. Please enter a number.");
                    continue;
                }

                try
                {
                    switch (choice)
                    {
                        case 1:
                            Console.Write("Customer ID: ");
                            if (!long.TryParse(Console.ReadLine(), out long custId))
                            {
                                Console.WriteLine("Invalid customer ID.");
                                break;
                            }

                            Customer customer = bank.GetCustomerById(custId);
                            if (customer == null)
                            {
                                Console.WriteLine("Customer not found.");
                                break;
                            }

                            Console.Write("Account Type (Savings/Current/ZeroBalance): ");
                            string accType = Console.ReadLine().Trim().ToLower();

                            if (accType != "savings" && accType != "current" && accType != "zerobalance")
                            {
                                Console.WriteLine("Invalid account type.");
                                break;
                            }

                            accType = char.ToUpper(accType[0]) + accType.Substring(1);

                            Console.Write("Initial Balance: ");
                            if (!decimal.TryParse(Console.ReadLine(), out decimal balance))
                            {
                                Console.WriteLine("Invalid balance.");
                                break;
                            }

                            bank.CreateAccount(customer, accType, balance);
                            Console.WriteLine("Account created successfully!");
                            break;

                        case 2:
                            Console.Write("Account Number: ");
                            if (!long.TryParse(Console.ReadLine(), out long accNoDeposit))
                            {
                                Console.WriteLine("Invalid account number.");
                                break;
                            }

                            Console.Write("Amount to Deposit: ");
                            if (!decimal.TryParse(Console.ReadLine(), out decimal depAmount))
                            {
                                Console.WriteLine("Invalid amount.");
                                break;
                            }

                            Console.Write("Description: ");
                            string depDesc = Console.ReadLine();

                            bank.Deposit(accNoDeposit, depAmount, depDesc);
                            Console.WriteLine("Deposit successful.");
                            break;

                        case 3:
                            Console.Write("Account Number: ");
                            if (!long.TryParse(Console.ReadLine(), out long accNoWithdraw))
                            {
                                Console.WriteLine("Invalid account number.");
                                break;
                            }

                            Console.Write("Amount to Withdraw: ");
                            if (!decimal.TryParse(Console.ReadLine(), out decimal withAmount))
                            {
                                Console.WriteLine("Invalid amount.");
                                break;
                            }

                            Console.Write("Description: ");
                            string withDesc = Console.ReadLine();

                            decimal updatedBalance = bank.Withdraw(accNoWithdraw, withAmount, withDesc);
                            Console.WriteLine($"Withdrawal successful. Updated Balance: ₹{updatedBalance}");
                            break;

                        case 4:
                            Console.Write("Enter Account Number: ");
                            if (!long.TryParse(Console.ReadLine(), out long accNum))
                            {
                                Console.WriteLine("Invalid account number.");
                                break;
                            }

                            bank.ViewTransactionsForAccount(accNum);
                            break;

                        case 5:
                            Console.Write("Enter Account Number: ");
                            if (!long.TryParse(Console.ReadLine(), out long balAcc))
                            {
                                Console.WriteLine("Invalid account number.");
                                break;
                            }

                            decimal balanceAmount = bank.GetBalance((int)balAcc);
                            Console.WriteLine($"Current Balance: ₹{balanceAmount}");
                            break;

                        case 6:
                            Console.Write("Sender Account Number: ");
                            if (!long.TryParse(Console.ReadLine(), out long fromAcc))
                            {
                                Console.WriteLine("Invalid sender account number.");
                                break;
                            }

                            Console.Write("Receiver Account Number: ");
                            if (!long.TryParse(Console.ReadLine(), out long toAcc))
                            {
                                Console.WriteLine("Invalid receiver account number.");
                                break;
                            }

                            Console.Write("Amount to Transfer: ");
                            if (!decimal.TryParse(Console.ReadLine(), out decimal transferAmt))
                            {
                                Console.WriteLine("Invalid amount.");
                                break;
                            }

                            Console.Write("Description: ");
                            string transferDesc = Console.ReadLine();

                            bank.TransferFunds((int)fromAcc, (int)toAcc, transferAmt);
                            Console.WriteLine("Transfer successful.");
                            break;

                        case 7:
                            Console.Write("Enter Account Number: ");
                            if (!long.TryParse(Console.ReadLine(), out long detailsAcc))
                            {
                                Console.WriteLine("Invalid account number.");
                                break;
                            }

                            bank.GetAccountDetails(detailsAcc);
                            break;

                        case 8:
                            List<Account> accounts = bank.ListAccounts();

                            if (accounts.Count == 0)
                            {
                                Console.WriteLine("No accounts found.");
                            }
                            else
                            {
                                Console.WriteLine("\n====== All Accounts ======");
                                foreach (Account acc in accounts)
                                {
                                    Console.WriteLine($"Account Number : {acc.AccountNumber}");
                                    Console.WriteLine($"Account Type   : {acc.AccountType}");
                                    Console.WriteLine($"Balance        : ₹{acc.Balance}");
                                    Console.WriteLine($"Customer Name  : {acc.Customer.FirstName} {acc.Customer.LastName}");
                                    Console.WriteLine($"Phone Number   : {acc.Customer.PhoneNumber}");
                                    Console.WriteLine($"Address        : {acc.Customer.CustomerAddress}");

                                    if (acc is SavingsAccount sa)
                                    {
                                        Console.WriteLine($"Interest Rate  : {sa.InterestRate}%");
                                    }
                                    else if (acc is CurrentAccount ca)
                                    {
                                        Console.WriteLine($"Overdraft Limit: ₹{ca.OverdraftLimit}");
                                    }

                                    Console.WriteLine("------------------------------");
                                }
                            }
                            break;


                        case 9:
                            Console.WriteLine("Exiting HMBank. Thank you!");
                            return;

                        default:
                            Console.WriteLine("Invalid choice. Please try again.");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Something went wrong: {ex.Message}");
                }
            }
        }
    }
}
