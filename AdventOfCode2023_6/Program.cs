// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

var inputSmall = @"Time:      7  15   30
Distance:  9  40  200";

var input = @"Time:        35     93     73     66
Distance:   212   2060   1201   1044";

var splitter = new char[] { '\t', ' ' };
var lines = input.Split(Environment.NewLine);
var times = lines[0].Split(splitter, StringSplitOptions.RemoveEmptyEntries).Skip(1).Select(Int128.Parse).ToArray();
var distances = lines[1].Split(splitter, StringSplitOptions.RemoveEmptyEntries).Skip(1).Select(Int128.Parse).ToArray();
if (times.Length != distances.Length)
{
    throw new Exception("Times and distances must be the same length");
}

Int128 result = 1;
var waysToWinCalculator = new WaysToWinCalculator(times);
for (int i = 0; i < times.Length; i++)
{
    Console.WriteLine($"Time: {times[i]} Distance: {distances[i]}");
    result*= waysToWinCalculator.CalculateWaysToWin(times[i], distances[i]);
}
Console.WriteLine($"Result: {result}");

var time = Int128.Parse(string.Join("", lines[0].Split(splitter, StringSplitOptions.RemoveEmptyEntries).Skip(1)));
var distance = Int128.Parse(string.Join("", lines[1].Split(splitter, StringSplitOptions.RemoveEmptyEntries).Skip(1)));
waysToWinCalculator=new WaysToWinCalculator(new Int128[] { time });
Console.WriteLine($"Result: {waysToWinCalculator.CalculateWaysToWin(time, distance)}");

public class WaysToWinCalculator
{
    private Dictionary<Int128, List<Int128>> WaysToRun = new Dictionary<Int128, List<Int128>>();

    public WaysToWinCalculator(Int128[] times)
    {
        foreach (var time in times)
        {
            GetWaysToRide(time);
        }
    }

    public void GetWaysToRide(Int128 time)
    {
        var result = new List<Int128>();
        for (Int128 i = 1; i < time; i++)
        {
            result.Add((time - i) * i);
        }
        WaysToRun.Add(time, result);
    }

    public Int128 CalculateWaysToWin(Int128 time, Int128 distance)
    {
        return WaysToRun[time].Where(x => x > distance).Count();
    }
}