using System;
using System.Text;

namespace CodingChallenge1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("********** Removing character in a string **********");
            Question1();
            Console.WriteLine("********************");
            Console.WriteLine("********** Swapping characters in a string **********");
            Question2();
            Console.WriteLine("********************");
            Console.WriteLine("********** Find max number out of three numbers **********");
            Question3();
            Console.WriteLine("********************");
            Console.Read();
        }

        static void Question1()
        {
            Console.WriteLine("Enter the string: ");
            StringBuilder s = new StringBuilder(Console.ReadLine());
            Console.WriteLine($"Enter the char index to be removed between 0 and {s.Length - 1}");
            int index = Convert.ToInt32(Console.ReadLine());
            s.Remove(index, 1);
            Console.WriteLine($"The string after removing char at {index}: {s}");
        }

        static void Question2()
        {
            Console.WriteLine("Enter the string: ");
            StringBuilder s = new StringBuilder(Console.ReadLine());
            char last = s[s.Length - 1];
            char first = s[0];
            s.Replace(first, last, 0, 1);
            s.Replace(last, first, s.Length - 1, 1);
            Console.WriteLine($"The string after swaping first and last characters is {s}");
        }

        static void Question3()
        {
            int[] arr = new int[3];
            for (int i = 0; i < 3; i++)
            {
                Console.WriteLine($"Enter the number {i + 1}");
                arr[i] = Convert.ToInt32(Console.ReadLine());
            }
            int max = Math.Max(arr[0], Math.Max(arr[1], arr[2]));
            Console.WriteLine($"The largest number is: {max}");
        }
    }
}
