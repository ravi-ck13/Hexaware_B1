using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BankingSystem.ControlStructures
{
    // ------------------------- Task 5: Password Validation -------------------------
    /*
        Task 5: Password Validation

        Write a program that prompts the user to create a password for their bank account.
        Implement if conditions to validate the password according to these rules:
        1. The password must be at least 8 characters long.
        2. It must contain at least one uppercase letter.
        3. It must contain at least one digit.
        4. Display appropriate messages to indicate whether their password is valid or not.
    */
    public class Task_5
    {
        public static void ValidatePassword()
        {
            Console.WriteLine("\n--- Bank Account Password Setup ---");

            while (true)
            {
                Console.Write("\nEnter your new password: ");
                string password = Console.ReadLine();

                // Check if the password is valid
                if (IsValidPassword(password))
                {
                    Console.WriteLine(" Password is valid! Your account is now secured.");
                    break; // Exit the loop if password is valid
                }
                else
                {
                    Console.WriteLine(" Invalid password! Please follow the rules:");
                    Console.WriteLine("   - At least 8 characters long.");
                    Console.WriteLine("   - At least one uppercase letter.");
                    Console.WriteLine("   - At least one digit.");
                }
            }
            Console.WriteLine("\nPress any key to return to the main menu...");
            Console.ReadKey();
            Console.WriteLine("\nPress any key to return to the main menu...");
            Console.ReadKey();
        }

        // Function to validate password using conditions
        private static bool IsValidPassword(string password)
        {
            // Check length
            if (password.Length < 8)
                return false;

            // Check if it contains at least one uppercase letter
            if (!Regex.IsMatch(password, "[A-Z]"))
                return false;

            // Check if it contains at least one digit
            if (!Regex.IsMatch(password, "[0-9]"))
                return false;

            return true;
        }
    }
}
