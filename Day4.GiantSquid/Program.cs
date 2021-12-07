using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;


IEnumerable<byte> GetRow(IEnumerable<IEnumerable<byte>> board, int index)
{
    return board.ElementAt(index);
}

IEnumerable<byte> GetColumn(IEnumerable<IEnumerable<byte>> board, int index)
{
    return board.Select(row => row.ElementAt(index));
}

IEnumerable<IEnumerable<byte>> EnumerateBoard(IReadOnlyCollection<IEnumerable<byte>> board)
{
    // First enumerate rows
    // board.SelectMany((t, i) => { })
    for (var i = 0; i < board.Count; i++)
    {
        yield return GetRow(board, i);
        yield return GetColumn(board, i);
    }
}

bool BoardWinner(IReadOnlyCollection<IEnumerable<byte>> board, ICollection<byte> winningNumbers)
{
    return EnumerateBoard(board).Any(r => r.All(winningNumbers.Contains));
}

List<List<byte>> ReadBoard(TextReader reader)
{
    var board = new List<List<byte>>();

    while (true)
    {
        var row = reader.ReadLine();
        if (string.IsNullOrEmpty(row))
            return board;

        board.Add(row.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(byte.Parse).ToList());
    }
}

(IList<byte>, List<List<List<byte>>>) ReadFile()
{
    using var reader = new StreamReader(new FileStream("input.txt", FileMode.Open));

    var winningNumbers = reader.ReadLine()!.Split(',').Select(byte.Parse).ToList();
    reader.ReadLine();

    var boards = new List<List<List<byte>>>();

    while (!reader.EndOfStream)
    {
        boards.Add(ReadBoard(reader));
    }

    return (winningNumbers, boards);
}

IEnumerable<byte> GetUnused(IEnumerable<IEnumerable<byte>> board, IEnumerable<byte> winningNumbers)
{
    return board.SelectMany(e => e).Except(winningNumbers);
}

int Part1()
{
    var (winningNumbers, boards) = ReadFile();

    for (var i = 1; i <= winningNumbers.Count; i++)
    {
        foreach (var board in boards)
        {
            var wn = winningNumbers.Take(i).ToList();
            if (BoardWinner(board, wn))
                return GetUnused(board, wn).Select(e => (int)e).Sum() * wn.Last();
        }
    }

    return -1;
}

// Console.WriteLine(Part1());

int Part2()
{
    var (winningNumbers, boards) = ReadFile();

    for (var i = 1; i <= winningNumbers.Count; i++)
    {
        var wn = winningNumbers.Take(i).ToList();
        var winners = boards.Where(b => BoardWinner(b, wn)).ToList();

        boards = boards.Except(winners).ToList();

        if (boards.Count == 0)
            return GetUnused(winners.Last(), wn).Select(e => (int)e).Sum() * wn.Last();
    }

    return -1;
}

Console.WriteLine(Part2());