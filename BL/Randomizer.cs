namespace BL;

public static class Randomizer
{
    public static int GetRandomNumber(int fromNumber, int toNumber)
    {
        Random rnd = new Random();
        return rnd.Next(fromNumber, toNumber + 1);
    }
}