using System;

namespace assignment2
{
    class arrays
    {
        static void Main()
        {
            Console.WriteLine("Array operations");
            ArrayOps();
            Console.WriteLine("---------------------------------------");
            Console.WriteLine("Marks Problem");
            MarksProblem();
            Console.WriteLine("---------------------------------------");
            Console.WriteLine("copy array");
            CopyArray();
            Console.WriteLine("---------------------------------------");
            Console.Read();
        }

        // 1. Write a  Program to assign integer values to an array  and then print avg, min , max
        static void ArrayOps()
        {
            Console.WriteLine("Enter the size: ");
            int n = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter the values: ");
            int[] arr = new int[n];
            for (int i = 0; i < n; i++)
                arr[i] = Convert.ToInt32(Console.ReadLine());

            int sum = arr[0], min = arr[0], max = arr[0];
            for (int i = 1; i < n; i++)
            {
                sum += arr[i];
                min = Math.Min(min, arr[i]);
                max = Math.Max(max, arr[i]);
            }
            double avg = (sum * 1.0) / n;
            Console.WriteLine($"avg: {avg}, min: {min}, max:{max}");
        }

        //  2.	Write a program in C# to accept ten marks and display the total, avg, min, max, asc, des
        static void MarksProblem()
        {
            int n = 10;
            Console.WriteLine("Enter the ten marks: ");
            int[] arr = new int[n];
            for (int i = 0; i < n; i++)
                arr[i] = Convert.ToInt32(Console.ReadLine());

            int sum = arr[0], min = arr[0], max = arr[0];
            for (int i = 1; i < n; i++)
            {
                sum += arr[i];
                min = Math.Min(min, arr[i]);
                max = Math.Max(max, arr[i]);
            }
            double avg = (sum * 1.0) / n;
            Console.WriteLine($"sum: {sum}, avg: {avg}, min: {min}, max:{max}");

            Array.Sort(arr);
            Console.WriteLine("Ascending order of marks: ");
            for (int i = 0; i < n; i++)
                Console.Write(arr[i] + " ");
            Console.WriteLine();

            int st = 0, end = n - 1;
            while (st < end)
            {
                arr[st] = arr[st] ^ arr[end];
                arr[end] = arr[st] ^ arr[end];
                arr[st] = arr[st] ^ arr[end];
                st++;
                end--;
            }

            Console.WriteLine("Descending order of marks: ");
            for (int i = 0; i < n; i++)
                Console.Write(arr[i] + " ");
            Console.WriteLine();
        }

        // 3.  Write a C# Sharp program to copy the elements of one array into another array.
        static void CopyArray()
        {
            Console.WriteLine("Enter the size: ");
            int n = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter the values: ");
            int[] arr = new int[n];
            for (int i = 0; i < n; i++)
                arr[i] = Convert.ToInt32(Console.ReadLine());

            //copy to another arrays
            int[] copy = new int[arr.Length];
            for (int i = 0; i < arr.Length; i++)
                copy[i] = arr[i];

            Console.WriteLine("Original Array: ");
            for (int i = 0; i < n; i++)
                Console.Write(arr[i] + " ");

            Console.WriteLine();
            Console.WriteLine("Copy Array: ");
            for (int i = 0; i < n; i++)
                Console.Write(copy[i] + " ");
            Console.WriteLine();
        }
    }
}
