using System;
using System.Collections.Generic;

public class PairShuffler
{
    public List<int> ShuffledList { get; private set; }

    public PairShuffler(int numberOfPairs)
    {
        ShuffledList = GenerateAndShufflePairs(numberOfPairs);
    }

    private List<int> GenerateAndShufflePairs(int count)
    {
        List<int> pairs = new List<int>();

        // Create the paired list: [1, 1, 2, 2, ..., count, count]
        for (int i = 1; i <= count; i++)
        {
            pairs.Add(i);
            pairs.Add(i);
        }

        // Shuffle using Fisher-Yates
        Random rng = new Random();
        int n = pairs.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            int value = pairs[k];
            pairs[k] = pairs[n];
            pairs[n] = value;
        }

        return pairs;
    }
}
