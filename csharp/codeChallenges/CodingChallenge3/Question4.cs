using System;

namespace CodingChallenge3
{
    /*
     * 4. Write a console program that uses delegate object as an argument to call Calculator Functionalities like 
     * 1. Addition, 2. Subtraction and 
     * 3. Multiplication by taking 2 integers and returning the output to the user.
     * You should display all the return values accordingly.
     */
    class Question4
    {
        public delegate int Calculator(int x, int y);
        public static int Add(int x, int y)
        {
            return x + y;
        }
        public static int Subtract(int x, int y)
        {
            return x - y;
        }
        public static int Multiply(int x, int y)
        {
            return x * y;
        }
        public static void Main(string[] args)
        {
            Calculator add = new Calculator(Add);
            Calculator subtract = new Calculator(Subtract);
            Calculator multiply = new Calculator(Multiply);

            while (true)
            {
                Console.WriteLine("Enter n to continue or any other key to exit");
                char ch = Convert.ToChar(Console.ReadLine());
                if (ch == 'n')
                {
                    Console.Write("Enter First Number: ");
                    int x = Convert.ToInt32(Console.ReadLine());
                    Console.Write("Enter Second Number: ");
                    int y = Convert.ToInt32(Console.ReadLine());

                    Console.WriteLine($"Addition: {add(x, y)}");
                    Console.WriteLine($"Subtraction: {subtract(x, y)}");
                    Console.WriteLine($"Multiplication: {multiply(x, y)}");
                }
                else
                {
                    break;
                }
            }
            Console.WriteLine("********** EXIT **********");
            Console.ReadLine();
        }
    }
}
