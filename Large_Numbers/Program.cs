
static void Calculate(int n, int m, string prefix = null)
{
    if (m == 0)
    {
        var halfBytes = prefix.Length / 4;
        var result = "0x";

        for (int i = 0; i < halfBytes; i++)
        {
            var halfByte = prefix.Skip(4 * i).Take(4).ToArray();
            var bitWeight = 0;

            for (int j = 0; j < halfByte.Length; j++)
            {
                if (halfByte[j] == '0')
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