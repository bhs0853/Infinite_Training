using System;

namespace CodingChallenge2
{
    class NegativeValueException : ApplicationException
    {
        public NegativeValueException(string message) : base(message) { }
    }
    class Question3
    {
        static void CheckPositive(int n)
        {
            if (n < 0)
                throw new NegativeValueException("Exception: Negative integer encountered");
            else
                Console.WriteLine("The number is positive");
        }
        static void Main(string[] args)
        {
            int num = 0;
            while (num == 0)
            {
                Console.Write("Enter the integer value greater than 0: ");
                bool isConverted = int.TryParse(Console.ReadLine(), out num);
                if (!isConverted)
                    Console.WriteLine("Invalid Format: Enter an integer value");
            }
            try
            {
                CheckPositive(num);
            }
            catch (NegativeValueException e)
            {
                Console.WriteLine(e.Message);
            }
            Console.ReadLine();
        }
    }
}
