using System.Numerics;
using Large_Numbers;

start: Console.Write("Enter key length in bits: ");

var input = Console.ReadLine();
var isCorrectInput = int.TryParse(input, out int keyLength);

if (!isCorrectInput || keyLength < 8)
{
    Console.WriteLine($"Incorrent input: {input}");
    goto start;
}

var keySpace = BigInteger.Pow(2, keyLength);

Console.WriteLine();
Console.WriteLine(keySpace);

var keyInfo = Console.ReadKey(intercept: true);

switch (keyInfo.Key)
{
    case ConsoleKey.S:
        Calculate(1, keyLength, printRequested: true);

        break;
    case ConsoleKey.G:
        var key = GenerateKey(keyLength);
        var task = FindAsync(1, keyLength, key);

        Console.WriteLine();
        Console.Write("Loading");

        while (!task.IsCompleted)
        {
            for (int i = 0; i < 4; i++)
            {
                Thread.Sleep(1000);
                Console.Write(".");
            }

            var location = Console.GetCursorPosition();

            Console.SetCursorPosition("Loading".Length, location.Top);
            Console.Write("    ");
            Console.SetCursorPosition("Loading".Length, location.Top);
        }

        var locationNew = Console.GetCursorPosition();

        Console.SetCursorPosition(0, locationNew.Top);
        Console.Write("                ");
        Console.SetCursorPosition(0, locationNew.Top);
        Console.WriteLine("Found");

        break;
}

Console.ReadKey();

static string GenerateKey(int keyLength)
{
    var bits = "";

    for (int i = 0; i < keyLength; i++)
    {
        var randInt = new Random().Next(0, 2);
        bits += randInt;
    }

    return ConvertToHex(bits);
}

static Task FindAsync(int n, int m, string key)
{
    var stopToken = new StopToken();
    return Task.Run(() => Calculate(n, m, keyToFind: key, token: stopToken));
}

static string ConvertToHex(string bits)
{
    var halfBytes = bits.Length / 4;
    var result = "0x";

    for (int i = 0; i < halfBytes; i++)
    {
        var halfByte = bits[(4 * i)..(4 * i + 4)];
        //var halfByte = bits.Skip(4 * i).Take(4).ToArray();
        var halfByteWeight = 0;

        for (int j = 0; j < halfByte.Length; j++)
        {
            if (halfByte[j] == '0')
                continue;

            var value = (int)Math.Pow(2, j);
            halfByteWeight += value;
        }

        string adding = halfByteWeight < 10 ? Convert.ToString(halfByteWeight) : Convert.ToString((char)(halfByteWeight + 55));
        result += adding;
    }

    return result;
}

static void Calculate(int n, int m, string prefix = null, string keyToFind = null, bool printRequested = false, StopToken token = null)
{
    if (m == 0)
    {
        var result = ConvertToHex(prefix);
        if (printRequested)
            Console.WriteLine(result);
        if (keyToFind != null)
        {
            if (result == keyToFind)
                token.RequestStop();
        }

        return;
    }

    for (int i = 0; i < n+1; i++)
    {
        prefix += i;
        Calculate(n, m - 1, prefix, keyToFind: keyToFind, token: token, printRequested: printRequested);

        if (token != null)
        {
            if (token.IsStopRequested)
                return;
        }

        prefix = prefix.Remove(prefix.Length - 1);
    }
}

Console.ReadKey();