namespace BankingSystem.InterfaceAndInheritance.App
{
    internal class BankApp
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            BankServiceProviderImpl bank = new BankServiceProviderImpl("HMBank", "Bangalore");

            while (true)
            {
                Console.WriteLine("\n--- Welcome to HMBank ---");
                Console.WriteLine("1. Create Account");
                Console.WriteLine("2. Deposit");
                Console.WriteLine("3. Withdraw");
                Console.WriteLine("4. Transfer");
                Console.WriteLine("5. Get Account Details");
                Console.WriteLine("6. List All Accounts");
                Console.WriteLine("7. Calculate Interest");
                Console.WriteLine("8. Exit");

                Console.Write("Enter choice: ");
                string choice = Console.ReadLine();

                try
                {
                    switch (choice)
                    {
                        case "1":
                            bank.CreateAccountInteractive();
                            break;

                        case "2":
                            Console.Write("Enter Account No: ");
                            long depAcc = long.Parse(Console.ReadLine());
                            Console.Write("Enter Deposit Amount: ");
                            float depAmt = float.Parse(Console.ReadLine());
                            float newDepBal = bank.Deposit(depAcc, depAmt);
                            Console.WriteLine($"New Balance: ₹{newDepBal:N2}");
                            break;

                        case "3":
                            Console.Write("Enter Account No: ");
                            long wdAcc = long.Parse(Console.ReadLine());
                            Console.Write("Enter Withdraw Amount: ");
                            float wdAmt = float.Parse(Console.ReadLine());
                            float newWdBal = bank.Withdraw(wdAcc, wdAmt);
                            Console.WriteLine($"New Balance: ₹{newWdBal:N2}");
                            break;

                        case "4":
                            Console.Write("Enter Sender Account No: ");
                            long fromAcc = long.Parse(Console.ReadLine());
                            Console.Write("Enter Receiver Account No: ");
                            long toAcc = long.Parse(Console.ReadLine());
                            Console.Write("Enter Amount to Transfer: ");
                            float transferAmt = float.Parse(Console.ReadLine());
                            bank.Transfer(fromAcc, toAcc, transferAmt);
                            break;

                        case "5":
                            Console.Write("Enter Account No: ");
                            long accNo = long.Parse(Console.ReadLine());
                            bank.GetAccountDetails(accNo);
                            break;

                        case "6":
                            bank.ListAccounts();
                            break;

                        case "7":
                            bank.CalculateInterest();
                            break;

                        case "8":
                            Console.WriteLine("Thank you for banking with us!");
                            return;

                        default:
                            Console.WriteLine("Invalid choice!");
                            break;
                    }
                }
                catch (InvalidAccountException ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
                catch (InsufficientFundException ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
                catch (OverDraftLimitExceededException ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
                catch (NullReferenceException)
                {
                    Console.WriteLine("Something went wrong: Null reference found!");
                }
                catch (FormatException)
                {
                    Console.WriteLine("Invalid input format. Please enter numeric values.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Unexpected error: {ex.Message}");
                }
            }
        }
    }
}
