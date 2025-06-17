using System;

namespace assignment1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Are Two Integers Equal");
            IsEqual();
            Console.WriteLine("-------------------------------------------------------");
            Console.WriteLine("Check if a number is postive or negative");
            CheckNumber();
            Console.WriteLine("-------------------------------------------------------");
            Console.WriteLine("Perform arthimetic operations on two numbers");
            PerformOperations();
            Console.WriteLine("-------------------------------------------------------");
            Console.WriteLine("Generate a Table for given number");
            GenerateTable();
            Console.WriteLine("-------------------------------------------------------");
            Console.WriteLine("Find the sum of two number");
            Console.WriteLine($"The sum is {findSum()}");
            Console.Read();
        }

        // 1. Write a C# Sharp program to accept two integers and check whether they are equal or not.
        static void IsEqual()
        {
            Console.WriteLine("Input 1st number:");
            int a = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Input 2nd number:");
            int b = Convert.ToInt32(Console.ReadLine());
            if (a == b)
                Console.WriteLine($"{a} and {b} are equal");
            else
                Console.WriteLine($"{a} and {b} are not equal");
        }

        // 2. Write a C# Sharp program to check whether a given number is positive or negative.
        static void CheckNumber()
        {
            Console.WriteLine("Enter the number:");
            int num = Convert.ToInt32(Console.ReadLine());
            if (num == 0)
                Console.WriteLine("The number is 0");
            else if (num > 0)
                Console.WriteLine($"{num} is a positive number");
            else
                Console.WriteLine($"{num} is a negative number");
        }

        // 3. Write a C# Sharp program that takes two numbers as input and performs all operations (+,-,*,/) on them and displays the result of that operation.
        static void PerformOperations()
        {
            Console.WriteLine("Input 1st number:");
            int a = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Input 2nd number:");
            int b = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine($"{a} + {b} = {a + b}");
            Console.WriteLine($"{a} + {b} = {a + b}");
            Console.WriteLine($"{a} - {b} = {a - b}");
            Console.WriteLine($"{a} * {b} = {a * b}");
            if (b != 0)
                Console.WriteLine($"{a} / {b} = {a * 1.0 / b}");
        }

        //4. Write a C# Sharp program that prints the multiplication table of a number as input.
        static void GenerateTable()
        {
            Console.WriteLine("Enter the number:");
            int num = Convert.ToInt32(Console.ReadLine());
            for (int i = 0; i <= 10; i++)
                Console.WriteLine($"{num} * {i} = {num * i}");
        }

        // 5.  Write a C# program to compute the sum of two given integers. If two values are the same, return the triple of their sum.
        static int findSum()
        {
            Console.WriteLine("Input 1st number:");
            int a = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Input 2nd number:");
            int b = Convert.ToInt32(Console.ReadLine());
            return a == b ? 3 * (a + b) : a + b;
        }

    }
}
