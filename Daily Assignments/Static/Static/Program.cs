using System.Diagnostics.Metrics;

namespace Static
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");

            Counter.CountFunction();
            Counter.CountFunction();
            Counter.CountFunction();

            Console.WriteLine("The CountFunction has been called {0} times", Counter.functionCallCount);
        }
    }
}
