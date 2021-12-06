using System;
using System.IO;
using System.Linq;

int Part1()
{
    int prev = -1;
    var count = 0;
    foreach (var line in File.ReadLines("input.txt"))
    {
        var curr = int.Parse(line);

        if (prev > 0)
        {
            count += curr > prev ? 1 : 0;
        }

        prev = curr;
    }

    return count;
}

// Console.WriteLine(Part1());

int Part2()
{
    var file = File.ReadLines("input.txt").Select(int.Parse).ToArray();

    var counter = 0;
    var prev = file.Take(3).Sum();
    for (var i = 1; i < file.Length - 2; i++)
    {
        var curr = file.Skip(i).Take(3).Sum();

        if (curr > prev)
            counter++;

        prev = curr;
    }

    return counter;
}

Console.WriteLine(Part2());