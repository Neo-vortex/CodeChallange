namespace CodeChallenge.Utilities;

public static class UtilityFunctions
{
   public static ulong CalculateHash(string read, bool lowTolerance)
    {
        ulong hashedValue = 0;
        var i = 0;
        ulong multiplier = 1;
        while (i < read.Length)
        {
            hashedValue += read[i] * multiplier;
            multiplier *= 37;
            if (lowTolerance) i += 2;
            else i++;
        }
        return hashedValue;
    }
}