using System;

namespace assignment2
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Swap two numbers");
            swap();
            Console.WriteLine("---------------------------------------");
            Console.WriteLine("Print pattern");
            pattern();
            Console.WriteLine("---------------------------------------");
            Console.WriteLine("Find day");
            enums();
            Console.WriteLine("---------------------------------------");
            Console.Read();
        }

        // 1. Write a C# Sharp program to swap two numbers.
        static void swap()
        {
            Console.WriteLine("Enter the 1st number");
            int a = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter the 2nd number");
            int b = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine($"a = {a}, b = {b}");
            a = a ^ b;
            b = a ^ b;
            a = a ^ b;
            Console.WriteLine($"a = {a}, b = {b}");
        }

        //2. Write a C# program that takes a number as input and displays it four times in a row (separated by blank spaces), and then four times in the next row, with no separation
        static void pattern()
        {
            Console.WriteLine("Enter the number");
            int num = Convert.ToInt32(Console.ReadLine());
            for (int i = 0; i < 4; i++)
            {
                if ((i & 1) > 0)
                    Console.WriteLine("{0}{1}{2}{3}", num, num, num, num);
                else
                    Console.WriteLine("{0} {1} {2} {3}", num, num, num, num);
            }
        }

        // 3. Write a C# Sharp program to read any day number as an integer and display the name of the day as a word.
        enum days { Monday = 1, Tuesday, Wednesday, Thrusday, Friday, Saturday, Sunday }
        static void enums()
        {
            Console.WriteLine("Enter the day number");
            int num = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine(Enum.GetName(typeof(days), num));
        }

    }
}
