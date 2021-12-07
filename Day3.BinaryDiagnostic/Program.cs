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
        {
            arr[binLen - i - 1] += (curr & (1 << i)) > 0 ? 1 : 0;
        }

        return arr;
    });

    var gamma = 0;
    for (var i = 0; i < binLen; i++)
    {
        gamma += (1 << i) * (counts[binLen - i - 1] * 2 / file.Count);
    }

    var epsilon = ~gamma & 0xFFF;

    return gamma * epsilon;
}

// Console.WriteLine(Part1()); // 738234

byte GetBit(int num, int idx)
{
    return (byte)((num & (1 << (binLen - idx - 1))) > 0 ? 1 : 0);
}

int GetBest(IList<int> best, int gamma)
{
    for (var i = 0; i < binLen; i++)
    {
        best = best.Where(b => GetBit(b, i) == GetBit(gamma, i)).ToList();

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
        {
            arr[binLen - i - 1] += (curr & (1 << i)) > 0 ? 1 : 0;
        }

        return arr;
    });

    var gamma = 0;
    for (var i = 0; i < binLen; i++)
    {
        gamma += (1 << i) * (counts[binLen - i - 1] * 2 / file.Count);
    }

    var epsilon = ~gamma & 0xFFF;


    var oxygen = GetBest(file, gamma);
    var co2 = GetBest(file, epsilon);


    return oxygen * co2;
}

Console.WriteLine(Part2()); // 738423