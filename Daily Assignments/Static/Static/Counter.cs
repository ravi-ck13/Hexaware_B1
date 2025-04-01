using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Static
{
    class Counter
    {
        public static int functionCallCount = 0;

        public static void CountFunction()
        {
            functionCallCount++;
            Console.WriteLine("Count Function called!");
        }
    }
}
