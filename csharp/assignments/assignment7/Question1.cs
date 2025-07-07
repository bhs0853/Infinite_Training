using System;
using System.Collections.Generic;

namespace assignment7
{
    /*
     * 1.) Write a query that returns list of numbers and their squares only if square is greater than 20 
     */
    class Question1
    {
        static void Main(string[] args)
        {
            Console.Write("Enter the length of list: ");
            int len = Convert.ToInt32(Console.ReadLine());
            List<int> list = new List<int>();
            for (int i = 0; i < len; i++)
            {
                try
                {
                    Console.Write($"Enter the element {i + 1}: ");
                    list.Add(Convert.ToInt32(Console.ReadLine()));
                }
                catch (Exception e)
                {
                    i--;
                    Console.WriteLine(e.Message);
                }
            }
            Console.Write("Enter the min sqaure value required: ");
            int min = Convert.ToInt32(Console.ReadLine());
            List<int> result = list.FindAll(n => (n * n) > min);
            Console.WriteLine("The squares of numbers which are greater than 20");
            foreach (int num in result)
                Console.Write(num + " ");
            Console.WriteLine();
            Console.WriteLine("Press any key to exit");
            Console.ReadLine();
        }
    }
}
