using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inheritance1
{
    public class Student
    {
        // Data members (Fields)
        public int RollNo;
        public string Name;
        public string Class;
        public string Semester;
        public string Branch;
        public int[] Marks = new int[5];

        // Constructor to initialize student details
        public Student(int rollNo, string name, string studentClass, string semester, string branch)
        {
            RollNo = rollNo;
            Name = name;
            Class = studentClass;
            Semester = semester;
            Branch = branch;
        }

        // Method to input marks for 5 subjects
        public void GetMarks()
        {
            for (int i = 0; i < Marks.Length; i++)
            {
                Console.Write($"Enter marks for subject {i + 1}: ");
                Marks[i] = Convert.ToInt32(Console.ReadLine());
            }
        }

        // Method to calculate result
        public void DisplayResult()
        {
            int total = 0;
            bool hasFailed = false;

            // Check marks and calculate total
            for (int i = 0; i < Marks.Length; i++)
            {
                if (Marks[i] < 35)
                {
                    hasFailed = true;
                }
                total += Marks[i];
            }

            // Calculate average
            double average = total / 5.0;

            // Print result based on conditions
            if (hasFailed)
            {
                Console.WriteLine("Result: Failed (Marks below 35 in one or more subjects)");
            }
            else if (average < 50)
            {
                Console.WriteLine("Result: Failed (Average marks below 50)");
            }
            else
            {
                Console.WriteLine("Result: Passed 🎉");
            }
        }

        // Method to display student details
        public void DisplayData()
        {
            Console.WriteLine("\nStudent Details:");
            Console.WriteLine($"Roll No: {RollNo}");
            Console.WriteLine($"Name: {Name}");
            Console.WriteLine($"Class: {Class}");
            Console.WriteLine($"Semester: {Semester}");
            Console.WriteLine($"Branch: {Branch}");
            Console.WriteLine("Marks: " + string.Join(", ", Marks));
        }
    }
}