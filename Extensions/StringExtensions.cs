public static class StringExtensions 
{
    public static int[] ToIntArray(this string str)
    {
        var output = new int[str.Length];

        for (var i = 0; i < str.Length; i++)
        {
            output[i] = str[i].ToInteger();
        }

        return output;
    }
}