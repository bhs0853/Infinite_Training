using System;

namespace CodingChallenge2
{
    /*
     * 1. Create an Abstract class Student with  Name, StudentId, Grade as members and also an abstract method Boolean Ispassed(grade) which takes grade as an input and checks whether student passed the course or not.  
 
    * Create 2 Sub classes Undergraduate and Graduate that inherits all members of the student and overrides Ispassed(grade) method
 
    * For the UnderGrad class, if the grade is above 70.0, then isPassed returns true, otherwise it returns false. For the Grad class, if the grade is above 80.0, then isPassed returns true, otherwise returns false.
 
    * Test the above by creating appropriate objects
     */
    abstract class Student
    {
        public string Name;
        public int StudentId;
        public double grade;
        public abstract Boolean IsPassed(double grade);
    }
    class UnderGraduate : Student
    {
        public UnderGraduate(string Name, int StudentId, double grade)
        {
            this.Name = Name;
            this.StudentId = StudentId;
            this.grade = grade;
        }
        public override bool IsPassed(double grade)
        {
            return grade > 70.0 ? true : false;
        }
    }
    class Graduate : Student
    {
        public Graduate(string Name, int StudentId, double grade)
        {
            this.Name = Name;
            this.StudentId = StudentId;
            this.grade = grade;
        }
        public override bool IsPassed(double grade)
        {
            return grade > 80.0 ? true : false;
        }
    }
    class Question1
    {
        static void Main(string[] args)
        {
            Console.Write("Enter the student name: ");
            string Name = Console.ReadLine();
            Console.Write("Enter the student id: ");
            int StudentId = Convert.ToInt32(Console.ReadLine());
            Console.Write("Enter the student grade: ");
            double grade = Convert.ToInt32(Console.ReadLine());
            Console.Write("Enter student class: u for undergraduation and g for graduation: ");
            char Class = char.Parse(Console.ReadLine());
            if (Class == 'u')
            {
                UnderGraduate ugStudent = new UnderGraduate(Name, StudentId, grade);
                Console.WriteLine($"The undergraduate student {(ugStudent.IsPassed(grade) ? "passed" : "failed")}");
            }
            else if (Class == 'g')
            {
                Graduate graduateStudent = new Graduate(Name, StudentId, grade);
                Console.WriteLine($"The graduate student {(graduateStudent.IsPassed(grade) ? "passed" : "failed")}");
            }
            else
            {
                Console.WriteLine("Invalid character");
            }
            Console.ReadLine();
        }
    }
}
