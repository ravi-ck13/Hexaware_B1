using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interface
{
    public class DayScholar : IStudent
    {
        
        public int StudentId { get; set; }
        public string Name { get; set; }
        public double Fees { get; set; }

        public DayScholar(int studentId, string name, double fees)
        {
            StudentId = studentId;
            Name = name;
            Fees = fees;
        }

        public void ShowDetails()
        {
            Console.WriteLine($"DayScholar - ID: {StudentId}, Name: {Name}, Fees: {Fees}");
        }
    }
}
