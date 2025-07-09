using System;

namespace CodeChallenge3
{
    /*
     * 2. Write a class Box that has Length and breadth as its members. Write a function that adds 2 box objects and stores in the 3rd.
     *    Display the 3rd object details. Create a Test class to execute the above.
     */
    class Box
    {
        public int Length;
        public int Breadth;
        public static Box operator +(Box b1, Box b2)
        {
            Box newBox = new Box();
            newBox.Length = b1.Length + b2.Length;
            newBox.Breadth = b1.Breadth + b2.Breadth;
            return newBox;
        }
    }
    class Test
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Operator overloading");
            while (true)
            {
                try
                {
                    Console.WriteLine("Enter 'n' to add two boxes or any other key to exit");
                    char ch = Convert.ToChar(Console.ReadLine());
                    if (ch == 'n')
                    {
                        Console.WriteLine("Enter the details of two boxes");
                        Box[] box = new Box[2];
                        for (int i = 0; i < 2; i++)
                        {
                            box[i] = new Box();
                            Console.WriteLine($"Enter the length of box{i + 1}");
                            bool length = int.TryParse(Console.ReadLine(), out box[i].Length);
                            Console.WriteLine($"Enter the breadth of box{i + 1}");
                            bool breadth = int.TryParse(Console.ReadLine(), out box[i].Breadth);
                            if (!length || !breadth)
                            {
                                i--;
                                Console.WriteLine("Please enter integer values");
                            }
                        }
                        Box newBox = box[0] + box[1];
                        Console.WriteLine($"new box details: Length: {newBox.Length} Breadth: {newBox.Breadth}");
                    }
                    else
                    {
                        break;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            Console.WriteLine("********** EXIT **********");
            Console.ReadLine();
        }
    }
}
