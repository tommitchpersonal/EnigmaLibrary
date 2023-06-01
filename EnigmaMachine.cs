namespace EnigmaLibrary;
public class EnigmaMachine : IEnigmaMachine
{
    public EnigmaSettings? Settings;
    private readonly IWheelFactory _wheelFactory = new WheelFactory();
    private readonly IComponent _reflector = new Reflector();
    private IWheel[]? _wheels;

    private int _numberOfLettersEncrypted = 1;

    public string Encrypt(string plainText)
    {
        CheckWhetherWheelsAreNull();

        var plainTextAsNumbers = plainText.ToIntArray();
        var cipherTextAsNumbers = new int[plainTextAsNumbers.Length];

        for (var i = 0; i < plainTextAsNumbers.Count(); i++)
        {
            cipherTextAsNumbers[i] = MapSingleNumber(plainTextAsNumbers[i]);
        }

        var cipherText = cipherTextAsNumbers.ArrayToString(); 

        Reset();

        return cipherText;
    }

    public char EncryptCharacter(char inputCharacter)
    {
        var asNumber = inputCharacter.ToInteger();

        var outputCharacter = MapSingleNumber(asNumber);

        return outputCharacter.ToCharacter();
    }



    public void UpdateSettings(EnigmaSettings newSettings)
    {
        Settings = newSettings;

        if (newSettings?.WheelSettings == null)
        {
            return;
        }

        _wheels = new IWheel[newSettings.WheelSettings.Count()];

        for (int i = 0; i < newSettings?.WheelSettings?.Count(); i++)
        {
            _wheels[i] = _wheelFactory.BuildWheel(newSettings.WheelSettings[i]);
        }
    }

    public void Reset()
    {
        CheckWhetherWheelsAreNull();

        foreach (var wheel in _wheels!)
        {
            wheel.Reset();
        }

        _numberOfLettersEncrypted = 1;
    }

    public EnigmaSettings? GetSettings()
    {
        return Settings;
    }

    private int MapSingleNumber(int inputLetter)
    {
        if (inputLetter > 25)
        {
            return inputLetter;
        }

        CheckWhetherWheelsAreNull();

        for (var j = 0; j < _wheels!.Count(); j++)
        {
            inputLetter = _wheels![j].Map(inputLetter);
        }

        inputLetter = _reflector.Map(inputLetter);

        for (var j = _wheels!.Count() - 1; j >= 0; j--)
        {
            inputLetter = _wheels![j].ReverseMap(inputLetter);

            if (ShouldRotateWheel(j))
            {
                _wheels[j].Rotate();
            }
        }

        _numberOfLettersEncrypted ++;

        return inputLetter;
    }

    private bool ShouldRotateWheel(int wheelNumber)
    {
        return  _numberOfLettersEncrypted % (Math.Pow(26, wheelNumber)) == 0;
    }

 

    private void CheckWhetherWheelsAreNull()
    {
        if (_wheels == null)
        {
            throw new EncryptionException("Enigma machine wheels have not been initialised. Try setting the wheels by making a put request to the update endpoint.");
        }
    }
}