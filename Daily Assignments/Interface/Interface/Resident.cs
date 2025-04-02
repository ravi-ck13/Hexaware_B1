using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interface
{
    public class Resident : IStudent
    {
 
        public int StudentId { get; set; }
        public string Name { get; set; }
        public double Fees { get; set; }

        public double AccommodationFees { get; set; }

        public Resident(int studentId, string name, double fees, double accommodationFees)
        {
            StudentId = studentId;
            Name = name;
            Fees = fees;
            AccommodationFees = accommodationFees;
        }
        public void ShowDetails()
        {
            double totalFees = Fees + AccommodationFees;
            Console.WriteLine($"Resident - ID: {StudentId}, Name: {Name}, Fees: {totalFees} Accommodation Fees: {AccommodationFees}");
        }
    }
}
