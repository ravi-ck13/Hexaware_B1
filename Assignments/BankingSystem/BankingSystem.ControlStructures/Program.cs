namespace BankingSystem.ControlStructures
{
    internal class Program
    {
        static void Main(string[] args)
        {
            bool continueRunning = true;
            while (continueRunning)
            {
                Console.Clear();
                Console.WriteLine("========== Banking System - Control Structure Tasks ==========");
                Console.WriteLine("1. Check Loan Eligibility");
                Console.WriteLine("2. ATM Transaction");
                Console.WriteLine("3. Compound Interest Calculator");
                Console.WriteLine("4. Account Balance Checker");
                Console.WriteLine("5. Password Validation");
                Console.WriteLine("6. Transaction List Manager");
                Console.WriteLine("7. Exit");
                Console.Write("Enter the number of the task to run: ");

                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        Task_1.CheckLoanEligibility();
                        break;
                    case "2":
                        Task_2.ATMTransaction();
                        break;
                    case "3":
                        Task_3.CalculateFutureBalance();
                        break;
                    case "4":
                        Task_4.AccountBalanceChecker();
                        break;
                    case "5":
                        Task_5.ValidatePassword();
                        break;
                    case "6":
                        Task_6.ManageTransactions();
                        break;
                    case "7":
                        continueRunning = false;
                        break;
                    default:
                        Console.WriteLine("Invalid option. Press Enter to continue...");
                        Console.ReadLine();
                        break;
                }
            }
        }
        
        
        
        /*
         * =================== Task 3: Compound Interest Calculator ===================
         
         * You are responsible for calculating compound interest on savings accounts 
         * for bank customers. The program should:
         * 1. Allow the user to input multiple customers' data.
         * 2. Ask for the initial balance, annual interest rate, and number of years.
         * 3. Use a loop structure (for loop) to calculate the future balance for each customer.
         * 4. Calculate the future balance using the formula: 
         *      future_balance = initial_balance * (1 + annual_interest_rate/100)^years
         * 5. Display the final balance for each customer.
         */
        static void CompoundInterestCalculator()
        {
            Console.Clear(); // Clears the console for better readability
            Console.WriteLine("===== Task 3: Compound Interest Calculator =====");

            // Asking the user for the number of customers
            Console.Write("Enter the number of customers: ");
            int numCustomers = Convert.ToInt32(Console.ReadLine());

            // Looping through multiple customers
            for (int i = 1; i <= numCustomers; i++)
            {
                Console.WriteLine($"\nCustomer {i}:");

                // Taking user inputs for balance, interest rate, and years
                Console.Write("Enter initial balance ($): ");
                double initialBalance = Convert.ToDouble(Console.ReadLine());

                Console.Write("Enter annual interest rate (%): ");
                double annualInterestRate = Convert.ToDouble(Console.ReadLine());

                Console.Write("Enter number of years: ");
                int years = Convert.ToInt32(Console.ReadLine());

                // Formula to calculate compound interest
                double futureBalance = initialBalance * Math.Pow((1 + annualInterestRate / 100), years);

                // Displaying the final balance for the customer
                Console.WriteLine($"Future Balance after {years} years: ${futureBalance:F2}");
            }
            Console.WriteLine("\nPress Enter to return to the main menu...");
            Console.ReadLine();
        }
        /*
        * =================== Task 4: Account Balance Check ===================
        * You need to create a program that:
        * 1. Stores multiple customer accounts with their balances.
        * 2. Uses a loop to repeatedly ask the user for their account number.
        * 3. Validates the account number entered by the user.
        * 4. If the account number is valid, display the account balance.
        * 5. If not, ask the user to try again.
        */

        static void AccountBalanceChecker()
        {
            Console.Clear(); // Clears the console for better readability
            Console.WriteLine("===== Task 4: Account Balance Check =====");

            // Storing account details in a Dictionary (Key: Account Number, Value: Balance)
            Dictionary<int, double> customerAccounts = new Dictionary<int, double>()
            {
                { 101, 5000.75 },
                { 102, 12345.50 },
                { 103, 876.20 },
                { 104, 1900.00 },
                { 105, 6723.45 }
            };

            while (true) // Infinite loop until a valid account is entered
            {
                Console.Write("\nEnter your account number: ");
                int accountNumber;
                bool isValidInput = int.TryParse(Console.ReadLine(), out accountNumber);

                if (isValidInput && customerAccounts.ContainsKey(accountNumber))
                {
                    Console.WriteLine($" Your account balance: ${customerAccounts[accountNumber]:F2}");
                    break; // Exit loop when valid account is found
                }
                else
                {
                    Console.WriteLine(" Invalid account number! Please try again.");
                }
            }

            // Prompting user to return to the main menu
            Console.WriteLine("\nPress Enter to return to the main menu...");
            Console.ReadLine();
        }
    }
}
