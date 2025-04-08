using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingSystem.ControlStructures
{
    // ------------------------- Task 1: Conditional Statements -------------------------
    /*
     In a bank, you have been given the task to create a program that checks if a customer is eligible for 
     a loan based on their credit score and income. The eligibility criteria are as follows:
     • Credit Score must be above 700.
     • Annual Income must be at least $50,000.

     Tasks:
     1. Write a program that takes the customer's credit score and annual income as input.
     2. Use conditional statements (if-else) to determine if the customer is eligible for a loan.
     3. Display an appropriate message based on eligibility.
    */
    public class Task_1
    {
        public static void CheckLoanEligibility()
        {
            Console.WriteLine("=== Loan Eligibility Checker ===");

            // Asking user for credit score
            Console.Write("Enter your credit score: ");
            int creditScore;
            bool validCredit = int.TryParse(Console.ReadLine(), out creditScore);

            // Asking user for annual income
            Console.Write("Enter your annual income : ");
            double annualIncome;
            bool validIncome = double.TryParse(Console.ReadLine(), out annualIncome);

            // Check if both inputs are valid
            if (!validCredit || !validIncome)
            {
                Console.WriteLine("Invalid input. Please enter valid numeric values.");
                return;
            }

            // Check eligibility using conditional statement
            if (creditScore > 700 && annualIncome >= 50000)
            {
                Console.WriteLine("Congratulations! You are eligible for a loan.");
            }
            else
            {
                Console.WriteLine("Sorry, you are not eligible for a loan.");
                if (creditScore <= 700)
                    Console.WriteLine("- Reason: Your credit score is too low.");
                if (annualIncome < 50000)
                    Console.WriteLine("- Reason: Your annual income is below the required threshold.");
            }
            Console.WriteLine("\nPress Enter to return to the main menu...");
            Console.ReadLine();
        }
    }
}
