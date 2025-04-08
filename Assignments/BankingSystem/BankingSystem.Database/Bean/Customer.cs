using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingSystem.DatabaseConnectivity.Bean
{
    public class Customer
    {
        public long CustomerId { get; set; }         // Maps to customer_id
        public string FirstName { get; set; }        // Maps to first_name
        public string LastName { get; set; }         // Maps to last_name
        public DateTime DOB { get; set; }            // Maps to DOB
        public string Email { get; set; }            // Maps to email
        public string PhoneNumber { get; set; }      // Maps to phone_number
        public string CustomerAddress { get; set; }  // Maps to customer_address

        public Customer() { }

        public Customer(long customerId, string firstName, string lastName, DateTime dob, string email, string phoneNumber, string customerAddress)
        {
            CustomerId = customerId;
            FirstName = firstName;
            LastName = lastName;
            DOB = dob;
            Email = email;
            PhoneNumber = phoneNumber;
            CustomerAddress = customerAddress;
        }

        public override string ToString()
        {
            return $"Customer ID: {CustomerId}, Name: {FirstName} {LastName}, Email: {Email}, Phone: {PhoneNumber}, Address: {CustomerAddress}";
        }
    }
}
