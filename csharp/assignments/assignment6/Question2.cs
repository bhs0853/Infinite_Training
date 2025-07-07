using System;
using System.IO;

namespace assignment6
{
    /*
     * 2. Write a program in C# Sharp to create a file and write an array of strings to the file.
     */
    class Question2
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter No of lines: ");
            int n = Convert.ToInt32(Console.ReadLine());
            string[] list = new string[n];
            for (int i = 0; i < n; i++)
            {
                Console.WriteLine($"Enter the line no {i + 1}");
                list[i] = Console.ReadLine();
            }

            Console.WriteLine("Enter the path in which file need to be created: ");
            string path = Console.ReadLine();
            Console.WriteLine();
            try
            {
                using (FileStream fs = new FileStream(path, FileMode.Create, FileAccess.Write))
                {
                    using (StreamWriter writer = new StreamWriter(fs))
                    {
                        foreach (string line in list)
                            writer.WriteLine(line);

                        Console.WriteLine($"File written successfully to: {path}");
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error writing file: " + e.Message);
            }
            Console.Read();
        }
    }
}
