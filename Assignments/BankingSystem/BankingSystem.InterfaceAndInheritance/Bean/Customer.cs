using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingSystem.InterfaceAndInheritance.Bean
{
    public class Customer
    {
        public int CustomerId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string AadhaarNumber { get; set; }
        public string PanNumber { get; set; }

        public Customer(int id, string name, string email, string phone, string address, string aadhaar, string pan)
        {
            CustomerId = id;
            Name = name;
            Email = email;
            Phone = phone;
            Address = address;
            AadhaarNumber = aadhaar;
            PanNumber = pan;
        }

        public void DisplayCustomerInfo()
        {
            Console.WriteLine($"Customer ID: {CustomerId}");
            Console.WriteLine($"Name: {Name}");
            Console.WriteLine($"Email: {Email}");
            Console.WriteLine($"Phone: {Phone}");
            Console.WriteLine($"Address: {Address}");
            Console.WriteLine($"Aadhaar: {AadhaarNumber}");
            Console.WriteLine($"PAN: {PanNumber}");
        }
    }
}
