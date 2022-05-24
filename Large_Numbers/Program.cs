
static void Calculate(int n, int m, string prefix = null)
{
    if (m == 0)
    {
        var halfByte = 4;
        var halfBytes = prefix.Length / 4;
        var result = "0x";

        for (int i = 1; i < halfBytes + 1; i++)
        {
            var t = "";
            var prefixCopy = new List<char>();

            var temp = "";

            var half = prefixCopy.Take(halfByte * i); // nado podumat kak eto normalno sdelat
            prefixCopy = prefix.Skip(halfByte * i).ToList();
            
            foreach (var elem in half)
                t += elem;

            var bitWeight = 0;

            for (int j = 0; j < t.Length; j++)
            {
                if (t[j] == '0')
                    continue;

                var value = (int)Math.Pow(2, j);

                bitWeight += value;
            }

            string adding = bitWeight < 10 ? Convert.ToString(bitWeight) : Convert.ToString((char)(bitWeight + 55));

            result += adding;
        }

        Console.WriteLine(result);
        return;
    }

    for (int i = 0; i < n+1; i++)
    {
        prefix += i;
        Calculate(n, m - 1, prefix);
        prefix = prefix.Remove(prefix.Length - 1);
    }
}

Calculate(1, 8);

Console.ReadKey();

static void PrintEnumerable<T>(IEnumerable<T> values)
{
    foreach (var value in values)
        Console.Write(value + " ");

    Console.WriteLine();
}