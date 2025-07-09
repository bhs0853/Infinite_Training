using System;
using System.IO;

namespace CodeChallenge3
{
    /*
     * 3. Write a program in C# Sharp to append some text to an existing file.
     *    If file is not available, then create one in the same workspace.
     */
    class Question3
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.Write("Enter the path of the file or e to exit: ");
                string path = Console.ReadLine();
                if (path == "e")
                    break;
                else
                {
                    Console.WriteLine("Enter the text to be appended");
                    string text = Console.ReadLine();
                    File.AppendAllText(path, text);
                }
            }
            Console.WriteLine("********** EXIT **********");
            Console.ReadLine();
        }
    }
}
