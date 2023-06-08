public interface IEnigmaMachine
{
    public EnigmaSettings? Settings {get;}
    public string Encrypt(string plainText);
    public char EncryptCharacter(char inputCharacter);
    public void UpdateSettings(EnigmaSettings newSettings);
    public void Reset();
}