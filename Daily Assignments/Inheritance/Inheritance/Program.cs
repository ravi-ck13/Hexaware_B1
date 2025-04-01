namespace Inheritance
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Employee employee = new Employee(101, "Rajesh", "2003-05-28", 45000);
            Manager manager = new Manager(100, "John", "1990-04-01", 50000, 5000, 10000);
            Employee mgr = new Manager(100, "John", "1990-04-01", 60000, 7000, 6000);

            Console.WriteLine(employee.ComputeSalary());
            Console.WriteLine(manager.ComputeSalary());
            Console.WriteLine(mgr.ComputeSalary());
        }
    }
}
