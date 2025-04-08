using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingSystem.OOP
{
    public class Customer
    {
        // Private attributes
        private int customerId;
        private string firstName;
        private string lastName;
        private string email;
        private string phoneNumber;
        private string address;

        // Default Constructor
        public Customer()
        {
            customerId = 0;
            firstName = "Unknown";
            lastName = "Unknown";
            email = "Unknown";
            phoneNumber = "Unknown";
            address = "Unknown";
        }

        // Parameterized Constructor
        public Customer(int id, string fName, string lName, string email, string phone, string address)
        {
            this.customerId = id;
            this.firstName = fName;
            this.lastName = lName;
            this.email = email;
            this.phoneNumber = phone;
            this.address = address;
        }

        // Getters and Setters
        public int GetCustomerId() => customerId;
        public void SetCustomerId(int id) => customerId = id;

        public string GetFirstName() => firstName;
        public void SetFirstName(string fName) => firstName = fName;

        public string GetLastName() => lastName;
        public void SetLastName(string lName) => lastName = lName;

        public string GetEmail() => email;
        public void SetEmail(string email) => this.email = email;

        public string GetPhoneNumber() => phoneNumber;
        public void SetPhoneNumber(string phone) => phoneNumber = phone;

        public string GetAddress() => address;
        public void SetAddress(string address) => this.address = address;

        // Method to display customer details
        public void PrintCustomerInfo()
        {
            Console.WriteLine("\nCustomer Details:");
            Console.WriteLine($"ID: {customerId}");
            Console.WriteLine($"Name: {firstName} {lastName}");
            Console.WriteLine($"Email: {email}");
            Console.WriteLine($"Phone: {phoneNumber}");
            Console.WriteLine($"Address: {address}");
        }
    }
}
