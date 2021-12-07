using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;

IEnumerable<(Vector2, Vector2)> ReadFile()
{
    var file = File.ReadLines("input.txt");
    return file.Select(l =>
    {
        var s = l.Split(" -> ");

        var v1 = s[0].Split(',').Select(int.Parse).ToList();
        var v2 = s[1].Split(',').Select(int.Parse).ToList();

        return (new Vector2(v1[0], v1[1]), new Vector2(v2[0], v2[1]));
    });
}

int Part1()
{
    var grid = new int[1000, 1000];
    foreach (var (v1, v2) in ReadFile())
    {
        if (!(v1.X == v2.X || v1.Y == v2.Y))
            continue;

        var dir = v2 - v1;
        var len = (int)dir.Length();
        dir /= len;

        for (var i = 0; i <= len; i++)
        {
            grid[(int)(v1.X + (i * dir).X), (int)(v1.Y + (i * dir).Y)]++;
        }
    }

    return grid.Cast<int>().Count(i => i > 1);
}

// Console.WriteLine(Part1());

int Part2()
{
    var grid = new int[1000, 1000];
    foreach (var (v1, v2) in ReadFile())
    {
        var dir = v2 - v1;
        var len = (int)Math.Max(Math.Abs(v2.X - v1.X), Math.Abs(v2.Y - v1.Y));
        dir /= len;

        for (var i = 0; i <= len; i++)
        {
            grid[(int)(v1.X + (i * dir).X), (int)(v1.Y + (i * dir).Y)]++;
        }
    }

    return grid.Cast<int>().Count(i => i > 1);
}

Console.WriteLine(Part2());