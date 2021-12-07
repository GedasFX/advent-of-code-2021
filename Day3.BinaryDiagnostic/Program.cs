using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

const int binLen = 12; // Taken from file. Manually counted.

int Part1()
{
    var file = File.ReadLines("input.txt").Select(s => Convert.ToInt32(s, 2)).ToList();
    var counts = file.Aggregate(new int[binLen], (arr, curr) =>
    {
        for (var i = 0; i < binLen; i++)
            arr[i] += GetBit(curr, i);

        return arr;
    });

    var gamma = 0;
    for (var i = 0; i < binLen; i++)
        gamma += (1 << i) * (counts[binLen - i - 1] * 2 / file.Count);

    var epsilon = ~gamma & ((1 << binLen) - 1);

    return gamma * epsilon;
}

Console.WriteLine(Part1()); // 738234

byte GetBit(int num, int idx)
{
    return (byte)((num & (1 << (binLen - idx - 1))) > 0 ? 1 : 0);
}

int GetBest(IList<int> best, byte tiebreaker)
{
    for (var i = 0; i < binLen; i++)
    {
        var i1 = i;
        var bitCount = best.Sum(b => GetBit(b, i1));

        var popBit = 2 * bitCount == best.Count ? 1 : bitCount * 2 / best.Count;
        if (tiebreaker == 0)
            popBit = popBit == 1 ? 0 : 1;

        best = best.Where(b => GetBit(b, i) == popBit).ToList();

        if (best.Count == 1)
            break;
    }

    return best.Single();
}

int Part2()
{
    var file = File.ReadLines("input.txt").Select(s => Convert.ToInt32(s, 2)).ToList();
    var counts = file.Aggregate(new int[binLen], (arr, curr) =>
    {
        for (var i = 0; i < binLen; i++)
            arr[i] += GetBit(curr, i);

        return arr;
    });

    var oxygen = GetBest(file, 1);
    var co2 = GetBest(file, 0);

    return oxygen * co2;
}

Console.WriteLine(Part2()); // 3969126