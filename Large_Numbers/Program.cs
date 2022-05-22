using System.Diagnostics;
using System.Text;

Task.Factory.StartNew(() =>
{
    var template1 = new List<string>()
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

    Calculate1(template1, expectedSequences);

    TimeSpan ts = stopWatch.Elapsed;
    var runtime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
            ts.Hours, ts.Minutes, ts.Seconds,
            ts.Milliseconds / 10);

    Console.WriteLine($"runtime of : {nameof(Calculate1)}" + runtime);
});

Task.Factory.StartNew(() =>
{
    var template2 = new List<string>()
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

    Calculate2(template2, expectedSequences);

    TimeSpan ts = stopWatch.Elapsed;
    var runtime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
            ts.Hours, ts.Minutes, ts.Seconds,
            ts.Milliseconds / 10);

    Console.WriteLine($"runtime of : {nameof(Calculate2)}" + runtime);
});


//foreach (var line in result)
//{
//    //var halfByte = 4;
//    //var halfBytes = line.Length / 4;

//    //for (int i = 1; i < halfBytes + 1; i++)
//    //{
//    //    var t = line.Take(halfByte * i);
//    //}

//    Console.WriteLine(line);
//}

//Console.WriteLine(result.Count);

Console.ReadKey();

static List<string> Calculate1(List<string> s, int num)
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

    return s.Count < num ? Calculate1(s, num) : s;
}

static List<string> Calculate2(List<string> s, int num)
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

    //s = s.Concat(buffer).ToList();

    foreach (var temp in buffer)
        s.Add(temp);

    return s.Count < num ? Calculate2(s, num) : s;
}

