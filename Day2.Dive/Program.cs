using System;
using System.IO;
using System.Linq;

long Part1()
{
    var file = File.ReadLines("input.txt").Select(s =>
    {
        var o = s.Split(' ');
        return new { Direction = o[0], Amount = int.Parse(o[1]) };
    });

    var dHorizontal = 0;
    var dVertical = 0;

    foreach (var instruction in file)
    {
        switch (instruction.Direction)
        {
            case "forward":
                dHorizontal += instruction.Amount;
                break;
            case "down":
                dVertical += instruction.Amount;
                break;
            case "up":
                dVertical -= instruction.Amount;
                break;
        }
    }

    return dHorizontal * dVertical;
}

// Console.WriteLine(Part1());

long Part2()
{
    var file = File.ReadLines("input.txt").Select(s =>
    {
        var o = s.Split(' ');
        return new { Direction = o[0], Amount = int.Parse(o[1]) };
    });

    var dHorizontal = 0;
    var dVertical = 0;
    var aim = 0;

    foreach (var instruction in file)
    {
        switch (instruction.Direction)
        {
            case "forward":
                dHorizontal += instruction.Amount;
                dVertical += aim * instruction.Amount;
                break;
            case "down":
                aim += instruction.Amount;
                break;
            case "up":
                aim -= instruction.Amount;
                break;
        }
    }

    return dHorizontal * dVertical;
}

Console.WriteLine(Part2());