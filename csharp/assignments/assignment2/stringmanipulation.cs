using System;
using System.Text;

namespace assignment2
{
    class stringmanipulation
    {
        static void Main()
        {
            Console.WriteLine("Find length of string");
            length();
            Console.WriteLine("---------------------------------------");
            Console.WriteLine("reverse a string");
            reverse();
            Console.WriteLine("---------------------------------------");
            Console.WriteLine("Check if two strings are equal");
            checkIfEqual();
            Console.WriteLine("---------------------------------------");
            Console.Read();
        }

        // 1.	Write a program in C# to accept a word from the user and display the length of it.
        static void length()
        {
            Console.WriteLine("Enter the word: ");
            string s = Console.ReadLine();
            Console.WriteLine($"Length of string {s} is {s.Length}");
        }

        // 2.	Write a program in C# to accept a word from the user and display the reverse of it.
        static void reverse()
        {
            Console.WriteLine("Enter the word: ");
            string s = Console.ReadLine();
            Console.WriteLine($"Input word: {s}");
            StringBuilder rev = new StringBuilder();
            for (int i = s.Length - 1; i >= 0; i--)
                rev.Append(s[i]);
            Console.WriteLine($"Reversed word: {rev}");
        }

        // 3.	Write a program in C# to accept two words from user and find out if they are same.
        static void checkIfEqual()
        {
            Console.WriteLine("Enter the first word: ");
            string a = Console.ReadLine();
            Console.WriteLine("Enter the second word: ");
            string b = Console.ReadLine();
            if (a.Equals(b))
                Console.WriteLine($"{a} & {b} are equal");
            else
                Console.WriteLine($"{a} & {b} are not equal");
        }
    }

}
