using log4net;

[assembly: log4net.Config.XmlConfigurator(ConfigFile = "log4net.config")]

namespace EnigmaLibrary;

public class EnigmaMachine : IEnigmaMachine
{
    public EnigmaSettings? Settings {get; private set;}
    private readonly IWheelFactory _wheelFactory = new WheelFactory();
    private readonly IComponent _reflector = new Reflector();
    private IWheel[]? _wheels;
    private ILog _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod()?.DeclaringType);

    private int _numberOfLettersEncrypted = 1;

    public EnigmaMachine(EnigmaSettings settings)
    {
        _log.Info("Generating new enigma machine");
        Settings = settings;

        SetWheels(settings);
    }

    public string Encrypt(string plainText)
    {
        CheckWhetherWheelsAreNull();

        _log.Info("Encrypting full string plain text");

        _log.Info($"Plain text: {plainText}");

        var plainTextAsNumbers = plainText.ToIntArray();

        var cipherTextAsNumbers = new int[plainTextAsNumbers.Length];

        for (var i = 0; i < plainTextAsNumbers.Count(); i++)
        {
            _log.Info($"Mapping: {plainTextAsNumbers[i]}");
            cipherTextAsNumbers[i] = MapSingleNumber(plainTextAsNumbers[i]);
        }

        var cipherText = cipherTextAsNumbers.ArrayToString(); 

        Reset();

        _log.Info($"Returning cipher text: {cipherText}");

        return cipherText;
    }

    public char EncryptCharacter(char inputCharacter)
    {
        _log.Info("Encrypting single character");

        var asNumber = inputCharacter.ToInteger();

        var outputInteger = MapSingleNumber(asNumber);

        var outputCharacter = outputInteger.ToCharacter();

        _log.Info($"Returning character {outputCharacter}");

        return outputCharacter;
    }

    public void UpdateSettings(EnigmaSettings newSettings)
    {
        _log.Info("Updating settings");

        Settings = newSettings;

        SetWheels(newSettings);
    }

    public void Reset()
    {
        _log.Info("Resetting settings");

        CheckWhetherWheelsAreNull();

        foreach (var wheel in _wheels!)
        {
            wheel.Reset();
        }

        _numberOfLettersEncrypted = 1;
    }

    private int MapSingleNumber(int inputLetter)
    {
        _log.Info("Mapping letter");

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
                _log.Info($"Rotating wheel {j}");
                _wheels[j].Rotate();
            }
        }

        _numberOfLettersEncrypted ++;
        
        _log.Info($"Letter mapped to {inputLetter}");

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
            _log.Error("Wheels settings are null in wheels.");
            return;
        }

        _wheels = new IWheel[settings.WheelSettings.Count()];

        for (int i = 0; i < settings?.WheelSettings?.Count(); i++)
        {
            _wheels[i] = _wheelFactory.BuildWheel(settings.WheelSettings[i]);
        }

        _log.Info("Wheels set.");
    }

    private void CheckWhetherWheelsAreNull()
    {
        if (_wheels == null)
        {
            _log.Error("Enigma machine wheels have not been initialised");
            throw new EncryptionException("Enigma machine wheels have not been initialised. Try setting the wheels by making a put request to the update endpoint.");
        }
    }
}