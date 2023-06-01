public static class IntExtensions
{
    public static char ToCharacter(this int input)
    {
        return input <= 25 ? (char) (input + (int)'A')  : " ".ToCharArray()[0];
    }
}