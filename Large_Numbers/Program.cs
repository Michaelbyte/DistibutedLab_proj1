
static void Calculate(int n, int m, string prefix = null)
{
    if (m == 0)
    {
        var halfBytes = prefix.Length / 4;
        var result = "0x";

        for (int i = 0; i < halfBytes; i++)
        {
            var t = "";

            var halfByte = prefix.Skip(4 * i).Take(4);
            
            foreach (var elem in halfByte)
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

Calculate(1, 64);

Console.ReadKey();

static void PrintEnumerable<T>(IEnumerable<T> values)
{
    foreach (var value in values)
        Console.Write(value + " ");

    Console.WriteLine();
}