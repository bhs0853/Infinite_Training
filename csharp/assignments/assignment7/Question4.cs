using System;
using ConcessionLibrary;

namespace assignment7
{
    /*
     * 4.    Create a class library with a function CalculateConcession()  that takes age as an input and calculates concession for travel as below:
            If age <= 5 then “Little Champs - Free Ticket” should be displayed
            If age > 60 then calculate 30% concession on the totalfare(Which is a constant Eg:500/-) and Display “ Senior Citizen” + Calculated Fare
            Else “Print Ticket Booked” + Fare. 
            Create a Console application with a Class called Program which has TotalFare as Constant, Name, Age.  Accept Name, Age from the user and call the CalculateConcession() function to test the Classlibrary functionality
     */
    class Question4
    {
        static void Main(string[] args)
        {
            Console.Write("Enter the name: ");
            string name = Console.ReadLine();
            Console.Write("Enter the age: ");
            int age = Convert.ToInt32(Console.ReadLine());
            int TotalFare = 500;
            ConcessionCalculator.calculateConcession(age, TotalFare);
            Console.Read();
        }
    }
}
