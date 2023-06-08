public static class CharacterExtensions
{
    public static int ToInteger(this char inputCharacter)
    {
        return char.IsLetter(inputCharacter) ? char.ToUpper(inputCharacter) - 'A' : 100;
    }

}