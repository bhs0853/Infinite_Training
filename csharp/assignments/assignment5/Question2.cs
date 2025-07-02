using System;

namespace assignment5
{
    /*
     * 2. Create a class called Scholarship which has a function Public void Merit() that takes marks and fees as an input. 
        If the given mark is >= 70 and <= 80, then calculate scholarship amount as 20% of the fees
        If the given mark is > 80 and <= 90, then calculate scholarship amount as 30% of the fees
        If the given mark is >90, then calculate scholarship amount as 50% of the fees.
        In all the cases return the Scholarship amount, else throw an user exception
     */
    class Scholarship
    {
        public int marks { get; set; }
        public int fees { get; set; }

        public int Merit(int marks, int fees)
        {
            this.marks = marks;
            this.fees = fees;

            double scholarshipAmount = 0;
            if (marks > 90)
                scholarshipAmount = 0.5 * fees;
            else if (marks > 80 && marks <= 90)
                scholarshipAmount = 0.3 * fees;
            else if (marks >= 70 && marks <= 80)
                scholarshipAmount = 0.2 * fees;
            else
                throw new ScholarshipNotApplicableException("Not Eligible for Scholarship : Minimum 70 marks required");
            return (int)scholarshipAmount;
        }
    }
    class Question2
    {
        static void Main(string[] args)
        {
            Console.WriteLine("***** Calculate Scholarship *****");
            int marks = 0;
            int fees = 0;
            while (marks == 0 || fees == 0)
            {
                try
                {
                    Console.Write("Enter the marks: ");
                    bool AreMarksConverted = int.TryParse(Console.ReadLine(), out marks);
                    Console.Write("Enter the fees: ");
                    bool IsFeeConverted = int.TryParse(Console.ReadLine(), out fees);
                    if (!AreMarksConverted || !IsFeeConverted)
                        throw new Exception("Incorrect format: Please enter integer values");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            Scholarship scholarship = new Scholarship();
            double scholarshipAmount = 0;
            try
            {
                scholarshipAmount = scholarship.Merit(marks, fees);
                Console.WriteLine($"The scholarship amount is {scholarshipAmount}");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            Console.ReadLine();
        }
    }
}
