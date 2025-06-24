using System;

namespace assignment3
{
    /*
     * 2. Create a class called student which has data members like rollno, name, class, Semester, branch, int [] marks=new int marks [5](marks of 5 subjects )

            -Pass the details of student like rollno, name, class, SEM, branch in constructor

            -For marks write a method called GetMarks() and give marks for all 5 subjects

            -Write a method called displayresult, which should calculate the average marks

            -If marks of any one subject is less than 35 print result as failed
            -If marks of all subject is >35,but average is < 50 then also print result as failed
            -If avg > 50 then print result as passed.

            -Write a DisplayData() method to display all object members values.

     */

    class Student
    {
        int rollNo { get; }
        String name { get; set; }
        int year { get; set; }

        int sem { get; set; }
        String branch { get; }

        int[] marks;

        public Student(int rollNo, String name, int year, int sem, String branch)
        {
            this.rollNo = rollNo;
            this.name = name;
            this.year = year;
            this.sem = sem;
            this.branch = branch;
            this.marks = new int[5];
        }

        // assign marks
        public void AssignMarks()
        {
            Console.WriteLine("Assigning marks...");
            for (int i = 0; i < marks.Length; i++)
            {
                Console.WriteLine($"Enter the marks of subject {i + 1}");
                marks[i] = Convert.ToInt32(Console.ReadLine());
            }
        }

        // display result
        public void DisplayResult()
        {
            Console.WriteLine("*********** Display student result ***********");
            int sum = 0;
            for (int i = 0; i < marks.Length; i++)
            {
                if (marks[i] < 35)
                {
                    Console.WriteLine("The student Failed");
                    return;
                }
                sum += marks[i];
            }
            int avg = sum / marks.Length;
            if (avg > 50)
                Console.WriteLine("The student Passed");
            else
                Console.WriteLine("The student Failed");
        }

        // display data
        public void DisplayData()
        {
            Console.WriteLine("********** Student Details **********");
            Console.WriteLine($"Roll no: {rollNo}");
            Console.WriteLine($"Name: {name}");
            Console.WriteLine($"class: {year}");
            Console.WriteLine($"sem: {sem}");
            Console.WriteLine($"branch: {branch}");
            Console.Write("The student marks: ");
            for (int i = 0; i < marks.Length; i++)
                Console.Write($"{marks[i]} ");
            Console.WriteLine();
        }
    }
    class StudentQuestion
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Student Details:");
            Console.WriteLine("Enter the roll no:");
            int rollNo = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter the Name:");
            String name = Console.ReadLine();
            Console.WriteLine("Enter the class:");
            int year = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter the Date of sem:");
            int sem = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter the branch:");
            String branch = Console.ReadLine();

            Student student = new Student(rollNo, name, year, sem, branch);
            student.AssignMarks();
            student.DisplayData();
            student.DisplayResult();
            Console.Read();
        }
    }
}
