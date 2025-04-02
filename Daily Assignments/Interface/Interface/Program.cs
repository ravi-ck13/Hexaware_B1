namespace Interface
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            IStudent dayScholar = new DayScholar(101, "Alice", 5000);
            dayScholar.ShowDetails();  
            IStudent resident = new Resident(102, "Bob", 6000, 2000); 
            resident.ShowDetails();
        }
    }
}
