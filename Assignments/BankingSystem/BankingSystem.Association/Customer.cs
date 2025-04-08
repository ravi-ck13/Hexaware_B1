using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BankingSystem.Association
{
    public class Customer
    {
        private static int customerCounter = 1001;

        public int CustomerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }

        // Default constructor
        public Customer() { }

        // Auto-ID constructor
        public Customer(string firstName, string lastName, string email, string phone, string address)
        {
            CustomerId = customerCounter++;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Phone = phone;
            Address = address;
        }

        public void DisplayCustomerInfo()
        {
            Console.WriteLine("\n--- Customer Information ---");
            Console.WriteLine($"Customer ID: {CustomerId}");
            Console.WriteLine($"Name: {FirstName} {LastName}");
            Console.WriteLine($"Email: {Email}");
            Console.WriteLine($"Phone: {Phone}");
            Console.WriteLine($"Address: {Address}");
        }
    }
}
