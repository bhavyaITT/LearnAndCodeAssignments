using System;
using System.Numerics;

class SubarrayMeanCalculator
{
    static void Main(string[] args)
    {
        var input = Array.ConvertAll(Console.ReadLine().Split(' '), int.Parse);
        int numberOfElements = input[0];
        int numberOfQueries = input[1];

        var arrayElements = Array.ConvertAll(Console.ReadLine().Split(' '), long.Parse);

        long[] prefixSum = new long[numberOfElements + 1];
        prefixSum[0] = 0;

        for (int i = 1; i <= numberOfElements; i++)
        {
            prefixSum[i] = prefixSum[i - 1] + arrayElements[i - 1];
        }

        for (int q = 0; q < numberOfQueries; q++)
        {
            var queryIndices = Array.ConvertAll(Console.ReadLine().Split(' '), int.Parse);
            int leftIndex = queryIndices[0];
            int rightIndex = queryIndices[1];

            long subarraySum = prefixSum[rightIndex] - prefixSum[leftIndex - 1];
            int subarrayLength = rightIndex - leftIndex + 1;

            long meanFloor = subarraySum / subarrayLength;

            Console.WriteLine(meanFloor);
        }
    }
}
