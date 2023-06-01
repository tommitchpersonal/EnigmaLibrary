using System.Text;

public static class IntArrayExtensions
{
    public static string ArrayToString(this int[] inputNumbers)
    {
        var sb = new StringBuilder();

        foreach (var number in inputNumbers)
        {
            sb.Append(number.ToCharacter());
        }

        return sb.ToString();
    }
}