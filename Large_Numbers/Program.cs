using System.Diagnostics;
using System.Text;

var template = new List<string>()
{
    "00",
    "01",
    "10",
    "11"
};

var bitsAmount = 16;
var expectedSequences = (int)Math.Pow(2, bitsAmount);
var stopWatch = new Stopwatch();
stopWatch.Start();

var task = CalculateAsync(template, expectedSequences);

Console.CursorVisible = false;
var loading = "Loading";
Console.Write(loading);

while (!task.IsCompleted)
{
    Console.SetCursorPosition(loading.Length, 0);

    for (int i = 0; i < 4; i++)
    {
        Thread.Sleep(1000);
        Console.Write(".");
    }

    Console.SetCursorPosition(loading.Length,0);

    for (int i = 0; i < 4; i++)
        Console.Write(" ");
}

Console.Clear();

foreach (var line in task.Result)
{
    //var halfByte = 4;
    //var halfBytes = line.Length / 4;

    //for (int i = 1; i < halfBytes + 1; i++)
    //{
    //    var t = line.Take(halfByte * i);
    //}

    Console.WriteLine(line);
}

Console.WriteLine(task.Result.Count);

Console.ReadKey();

static Task<List<string>> CalculateAsync(List<string> s, int num)
{
    return Task.Run(() => Calculate(s, num));
}

static List<string> Calculate(List<string> s, int num)
{
    var buffer = new List<string>();

    Action<string> addTop = (string first) =>
    {
        var sb = new StringBuilder();

        for (int i = 0; i < s.Count; i++)
        {
            var part = sb.AppendJoin("", first, s[i]).ToString();
            sb.Clear();
            buffer.Add(part);
        }
    };

    addTop("0");
    addTop("1");

    s.Clear();

    s = s.Concat(buffer).ToList();

    return s.Count < num ? Calculate(s, num) : s;
}