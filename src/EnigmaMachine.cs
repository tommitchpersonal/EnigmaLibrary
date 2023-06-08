namespace EnigmaLibrary;
public class EnigmaMachine : IEnigmaMachine
{
    public EnigmaSettings? Settings {get; private set;}
    private readonly IWheelFactory _wheelFactory = new WheelFactory();
    private readonly IComponent _reflector = new Reflector();
    private IWheel[]? _wheels;

    private int _numberOfLettersEncrypted = 1;

    public EnigmaMachine(EnigmaSettings settings)
    {
        Settings = settings;

        SetWheels(settings);
    }

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

        SetWheels(newSettings);
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

    private void SetWheels(EnigmaSettings settings)
    {
        if (settings?.WheelSettings == null)
        {
            return;
        }

        _wheels = new IWheel[settings.WheelSettings.Count()];

        for (int i = 0; i < settings?.WheelSettings?.Count(); i++)
        {
            _wheels[i] = _wheelFactory.BuildWheel(settings.WheelSettings[i]);
        }
    }

    private void CheckWhetherWheelsAreNull()
    {
        if (_wheels == null)
        {
            throw new EncryptionException("Enigma machine wheels have not been initialised. Try setting the wheels by making a put request to the update endpoint.");
        }
    }
}