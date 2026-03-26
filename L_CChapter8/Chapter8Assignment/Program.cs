using Chapter8Assignment;
using System;

namespace LearnAndCodeAssignmentChapter8;

public class Program
{
    public static void Main(string[] args)
    {
        Console.WriteLine("Enter the value of t (number of test cases):");
        int t = int.Parse(Console.ReadLine());

        while (t-- > 0)
        {
            Console.WriteLine("Enter the value of k:");
            int input = int.Parse(Console.ReadLine());
            int result = DivisorCounter.CountValidN(input);
            Console.WriteLine("Result "+result);
        }
    }
}