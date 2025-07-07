using System;

namespace ConcessionLibrary
{
    public class ConcessionCalculator
    {
        public static void calculateConcession(int age, int fare)
        {
            if (age <= 5)
                Console.WriteLine("Little Champs - Free Ticket");
            else if (age > 60)
                Console.WriteLine($"Senior Citizen fare: " + (fare * 0.7));
            else
                Console.WriteLine($"Ticket Booked: {fare}");
        }
    }
}
