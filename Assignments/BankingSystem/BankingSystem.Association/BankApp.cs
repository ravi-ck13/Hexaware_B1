namespace BankingSystem.Association
{
    internal class BankApp
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Bank bank = new Bank(); // create instance of Bank class
            bool exit = false;

            while (!exit)
            {
                Console.WriteLine("\n--- HMBank System Menu ---");
                Console.WriteLine("1. Create Account");
                Console.WriteLine("2. Deposit");
                Console.WriteLine("3. Withdraw");
                Console.WriteLine("4. Get Balance");
                Console.WriteLine("5. Transfer");
                Console.WriteLine("6. Get Account Details");
                Console.WriteLine("7. Exit");
                Console.Write("Enter your choice: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        bank.CreateAccount();  // Instance method
                        break;
                    case "2":
                        bank.Deposit();       // Instance method
                        break;
                    case "3":
                        bank.Withdraw();      // Instance method
                        break;
                    case "4":
                        bank.GetAccountBalance();  // Instance method
                        break;
                    case "5":
                        bank.Transfer();      // Instance method
                        break;
                    case "6":
                        bank.GetAccountDetails();  // Instance method
                        break;
                    case "7":
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }

            Console.WriteLine("Thank you for using HMBank. Goodbye!");
        }
    }
}
