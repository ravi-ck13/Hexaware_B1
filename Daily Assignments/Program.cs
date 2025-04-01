namespace ConsoleApp2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            /* --1--
            Console.Write("Input 1st number: ");
            int num1 = Convert.ToInt32(Console.ReadLine());

            Console.Write("Input 2nd number: ");
            int num2 = Convert.ToInt32(Console.ReadLine());

            if (num1 == num2)
                Console.WriteLine(num1+"and"+num2+"are equal.");
            else
                Console.WriteLine(num1+"and"+num2+"are not equal.");
            */

            /*  --2--
            Console.Write("Enter a number: ");
            int number = Convert.ToInt32(Console.ReadLine());
            if (number > 0)
            {
                Console.WriteLine($"{number} is a positive number.");
            }
            else if (number < 0)
            {
                Console.WriteLine($"{number} is a negative number.");
            }
            else
            {
                Console.WriteLine("The number is zero.");
            }
            */

            /* --3--
            Console.Write("Enter first number: ");
            double num1 = Convert.ToDouble(Console.ReadLine());

            Console.Write("Enter operator (+, -, *, /): ");
            char op = Convert.ToChar(Console.ReadLine());

            Console.Write("Enter second number: ");
            double num2 = Convert.ToDouble(Console.ReadLine());

            double result = 0;

            switch (op)
            {
                case '+':
                    result = num1 + num2;
                    break;
                case '-':
                    result = num1 - num2;
                    break;
                case '*':
                    result = num1 * num2;
                    break;
                case '/':
                    result = num1 / num2;
                    break;
                default:
                    Console.WriteLine("Invalid operator!");
                    return;
            }

            Console.WriteLine($"{num1} {op} {num2} = {result}");
            */

            /* --4--
            Console.Write("Enter the number: ");
            int num = Convert.ToInt32(Console.ReadLine());

            for (int i = 0; i <= 10; i++)
            {
                Console.WriteLine($"{num} * {i} = {num * i}");
            }
            */

            /* --5--
            Console.Write("Enter first number: ");
            int num1 = Convert.ToInt32(Console.ReadLine());

            Console.Write("Enter second number: ");
            int num2 = Convert.ToInt32(Console.ReadLine());

            int sum = num1 + num2;
            int result = (num1 == num2) ? sum * 3 : sum;

            Console.WriteLine($"Result: {result}");
            */

            /* set2 1
            Console.Write("Enter first number: ");
            int num1 = Convert.ToInt32(Console.ReadLine());
            Console.Write("Enter second number: ");
            int num2 = Convert.ToInt32(Console.ReadLine());
            int temp = num1;
            num1 = num2;
            num2 = temp;
            Console.WriteLine("yes" + num1);
            Console.WriteLine($"After swapping: First number = {num1}, Second number = {num2}");
            */

            /* 2
            Console.Write("Enter a digit: ");
            int num = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("{0} {0} {0} {0}", num);
            Console.WriteLine("{0}{0}{0}{0}", num);
            Console.WriteLine("{0} {0} {0} {0}", num);
            Console.WriteLine("{0}{0}{0}{0}", num);
            */

            /* 3
            Console.Write("Enter a day number (1-7): ");
            int day = Convert.ToInt32(Console.ReadLine());

            string dayName = day switch
            {
                1 => "Monday",
                2 => "Tuesday",
                3 => "Wednesday",
                4 => "Thursday",
                5 => "Friday",
                6 => "Saturday",
                7 => "Sunday",
                _ => "Invalid day number!"
            };

            Console.WriteLine(dayName);
            */
            /* Arrays 1
            int[] arr = { 10, 20, 30, 40, 50 };
            double average = arr.Average();
            int min = arr.Min();
            int max = arr.Max();

            Console.WriteLine($"Average Value: {average}");
            Console.WriteLine($"Minimum Value: {min}");
            Console.WriteLine($"Maximum Value: {max}");
            */
            /* 2
            int[] marks = new int[10];
            for (int i = 0; i < 10; i++)
            {
                Console.Write($"Enter mark {i + 1}: ");
                marks[i] = Convert.ToInt32(Console.ReadLine());
            }

            int total = 0, min = marks[0], max = marks[0];
            for (int i = 0; i < marks.Length; i++)
            {
                total += marks[i];
                if (marks[i] < min) min = marks[i];
                if (marks[i] > max) max = marks[i];
            }

            double average = (double)total / marks.Length;
            for (int i = 0; i < marks.Length - 1; i++)
            {
                for (int j = i + 1; j < marks.Length; j++)
                {
                    if (marks[i] > marks[j])
                    {
                        int temp = marks[i];
                        marks[i] = marks[j];
                        marks[j] = temp;
                    }
                }
            }

            Console.WriteLine($"Total Marks: {total}");
            Console.WriteLine($"Average Marks: {average}");
            Console.WriteLine($"Minimum Marks: {min}");
            Console.WriteLine($"Maximum Marks: {max}");

            Console.WriteLine("Marks in Ascending Order: " + string.Join(", ", marks));
            Array.Reverse(marks);
            Console.WriteLine("Marks in Descending Order: " + string.Join(", ", marks));
            */
            /* 3
            int[] originalArray = { 1, 2, 3, 4, 5 };
            int[] copiedArray = new int[originalArray.Length];
            for (int i = 0; i < originalArray.Length; i++)
            {
                copiedArray[i] = originalArray[i];
            }

            Console.WriteLine("Original Array: " + string.Join(", ", originalArray));
            Console.WriteLine("Copied Array: " + string.Join(", ", copiedArray));
            */
        }
    }
}
