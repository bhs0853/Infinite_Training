using System;
using System.IO;

namespace assignment6
{
    /*
     * 3. Write a program in C# Sharp to count the number of lines in a file.
     */
    class Question3
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter the path of the file: ");
            string path = Console.ReadLine();
            int count = 0;
            try
            {
                if (File.Exists(path))
                {
                    using (FileStream fs = new FileStream(path, FileMode.Open))
                    {
                        using (StreamReader sr = new StreamReader(fs))
                        {
                            while (sr.ReadLine() != null)
                                count++;
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Path not found");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            Console.WriteLine($"The count of number of lines in a file: {count}");
            Console.ReadLine();
        }
    }
}
