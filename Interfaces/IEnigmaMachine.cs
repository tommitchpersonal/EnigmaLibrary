public interface IEnigmaMachine
{
    public string Encrypt(string plainText);
    public char EncryptCharacter(char inputCharacter);
    public void UpdateSettings(EnigmaSettings newSettings);
    public void Reset();
    public EnigmaSettings? GetSettings();
}