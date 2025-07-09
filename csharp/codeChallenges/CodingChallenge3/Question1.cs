using System;

namespace CodeChallenge3
{

    /*
     * 1. Write a program to find the Sum and the Average points scored by the teams in the IPL.
     * Create a Class called CricketTeam that has a function called Pointscalculation(int no_of_matches) 
     * that takes no.of matches as input and accepts that many scores from the user. 
     * The function should then return the Count of Matches, Average and Sum of the scores.
     */
    class CricketTeam
    {
        public static string PointsCalculation(int noOfMatches)
        {
            int totalScore = 0;
            for (int i = 0; i < noOfMatches; i++)
            {
                try
                {
                    Console.WriteLine($"Enter the score of match {i + 1}");
                    totalScore += Convert.ToInt32(Console.ReadLine());
                }
                catch (Exception e)
                {
                    i--;
                    Console.WriteLine(e.Message);
                }
            }
            return $"Count of matches: {noOfMatches} Average: {totalScore * 1.0 / noOfMatches} Sum of scores: {totalScore}";
        }
    }
    class Question1
    {
        static void Main(string[] args)
        {
            Console.WriteLine("IPL Score Calculator");
            while (true)
            {
                Console.WriteLine("Press n to enter details of a team or any other key to exit");
                try
                {
                    char ch = Convert.ToChar(Console.ReadLine());
                    if (ch == 'n')
                    {
                        Console.Write("Enter name of the team: ");
                        string teamName = Console.ReadLine();
                        Console.WriteLine("Enter no of matches played");
                        int noOfMatches = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine($"Team {teamName} : {CricketTeam.PointsCalculation(noOfMatches)}");
                    }
                    else
                        break;
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
