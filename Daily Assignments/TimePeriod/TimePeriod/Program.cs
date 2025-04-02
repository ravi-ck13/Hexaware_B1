namespace TimePeriod
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            Time time = new Time(2.5);
            Console.WriteLine($"Time: {time.Hours} hours");
        }
    }
}
