using System;
using System.Collections.Generic;

namespace assignment7
{
    /*
     * 2.) Write a query that returns words starting with letter 'a' and ending with letter 'm'.
     */
    class Question2
    {
        static void Main(string[] args)
        {
            Console.Write("Enter the size of list: ");
            int n = Convert.ToInt32(Console.ReadLine());
            List<string> list = new List<string>();
            for (int i = 0; i < n; i++)
            {
                try
                {
                    Console.WriteLine($"Enter the string {i + 1}");
                    list.Add(Console.ReadLine());
                }
                catch (Exception e)
                {
                    i--;
                    Console.WriteLine(e.Message);
                }
            }
            Console.Write("Enter the desired character you want the string to start with: ");
            string start = Console.ReadLine();
            Console.Write("Enter the desired character you want the string to end with: ");
            string end = Console.ReadLine();
            List<string> result = list.FindAll(s => s.StartsWith(start) && s.EndsWith(end));
            Console.WriteLine($"The strings which starts with {start} and ends with {end} are: ");
            foreach (string s in result)
                Console.WriteLine(s);
            Console.ReadLine();
        }
    }
}
