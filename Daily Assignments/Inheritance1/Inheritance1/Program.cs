namespace Inheritance1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            Console.Write("Enter Roll Number: ");
            int rollNo = Convert.ToInt32(Console.ReadLine());

            Console.Write("Enter Name: ");
            string name = Console.ReadLine();

            Console.Write("Enter Class: ");
            string studentClass = Console.ReadLine();

            Console.Write("Enter Semester: ");
            string semester = Console.ReadLine();

            Console.Write("Enter Branch: ");
            string branch = Console.ReadLine();

            // Creating student object with details
            Student student = new Student(rollNo, name, studentClass, semester, branch);

            // Get marks from user
            student.GetMarks();

            // Display student data
            student.DisplayData();

            // Display student result
            student.DisplayResult();
        }
    }
}
