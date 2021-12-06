using System;
using System.IO;

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

Console.WriteLine(count);