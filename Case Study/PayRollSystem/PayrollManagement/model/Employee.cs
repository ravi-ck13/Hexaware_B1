using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayrollManagement.model
{
    public class Employee
    {
        // Basic properties
        public int EmployeeID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; }
        public string Mail { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string Position { get; set; }
        public DateTime JoiningDate { get; set; }

        // Needed for salary calculation
        public decimal BasicSalary { get; set; }
        public decimal OvertimePay { get; set; }      // ✅ renamed from "Overtime"
        public decimal Deductions { get; set; }

        // Optional TerminationDate
        private DateTime? _terminationDate;
        public DateTime? TerminationDate
        {
            get { return _terminationDate; }
            set
            {
                if (value.HasValue)
                {
                    _terminationDate = value;
                }
                else
                {
                    _terminationDate = DateTime.MinValue;
                }
            }
        }

        // Constructor
        public Employee() { }

        public Employee(int employeeID, string firstName, string lastName, DateTime dateOfBirth,
                        string gender, string mail, string phoneNumber, string address,
                        string position, DateTime joiningDate, DateTime terminationDate,
                        decimal basicSalary, decimal overtime)
        {
            EmployeeID = employeeID;
            FirstName = firstName;
            LastName = lastName;
            DateOfBirth = dateOfBirth;
            Gender = gender;
            Mail = mail;
            PhoneNumber = phoneNumber;
            Address = address;
            Position = position;
            JoiningDate = joiningDate;
            TerminationDate = terminationDate;
            BasicSalary = basicSalary;
            OvertimePay = overtime;
        }

        // Calculate age from DOB
        public int CalculateAge()
        {
            DateTime today = DateTime.Today;
            int age = today.Year - DateOfBirth.Year;
            if (today < DateOfBirth.AddYears(age))
                age--;
            return age;
        }

        // Override ToString() for displaying details
        public override string ToString()
        {
            return $"EmployeeID::{EmployeeID}\t FirstName::{FirstName}\t LastName::{LastName}\t DateOfBirth::{DateOfBirth.ToShortDateString()}\t " +
                   $"Gender::{Gender}\t Mail::{Mail}\t Phone::{PhoneNumber}\t Address::{Address}\t Position::{Position}\t " +
                   $"JoiningDate::{JoiningDate.ToShortDateString()}\t TerminationDate::{TerminationDate?.ToShortDateString()}\t " +
                   $"BasicSalary::{BasicSalary}\t Overtime::{OvertimePay}";
        }
    }
}
