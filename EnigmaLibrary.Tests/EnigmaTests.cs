using System.Text.Json;

[TestFixture]
public class EnigmaTests
{
    private EnigmaSettings? _testSettings = ReadTestSettings();

    [Test]
    public void SettingsAreCorrectWhenConstructed()
    {
        // Execute
        if (_testSettings == null)
        {
            return;
        }

        var sut = new EnigmaMachine(_testSettings);

        // Assert
        Assert.That(sut.Settings, Is.EqualTo(_testSettings));
    }

    [Test]
    public void EncryptsCorrectly()
    {
        // Arrange 
        const string input = "TEST";
        const string expectedOutput = "JDOP";

        CommonEncryptionTest(input, expectedOutput);
    }

    [Test]
    public void LowerCaseIsAccepted()
    {
        // Arrange 
        const string input = "test";
        const string expectedOutput = "JDOP";

        CommonEncryptionTest(input, expectedOutput);
    }

    [Test]
    public void SpecialCharactersNumbersAndSpacesReturnedAsSpaces()
    {
        // Arrange
        const string input = "1? @)";
        const string expectedOutput = "     ";

        CommonEncryptionTest(input, expectedOutput);
    }

    [Test]
    public void CallingEncryptIsIdempotent()
    {
        // Arrange 
        const string input = "TEST";
        const string expectedOutput = "JDOP";

        // By calling twice we check that machine automatically resets
        CommonEncryptionTest(input, expectedOutput);
        CommonEncryptionTest(input, expectedOutput);
    }

    [Test]
    public void DoesNotResetWhenSingleCharacterIsEncrypted()
    {
        // Arrange
        const char inputChar = 'A';

        if (_testSettings == null)
        {
            return;
        }

        // Execute
        var sut = new EnigmaMachine(_testSettings);
        var firstOutput = sut.EncryptCharacter(inputChar);
        var secondOutput = sut.EncryptCharacter(inputChar);

        // Assert
        Assert.That(firstOutput, Is.Not.EqualTo(secondOutput));
    }

    [Test]
    public void ResetsWhenResetIsCalled()
    {
        // Arrange 
        const char inputChar = 'A';

        if (_testSettings == null)
        {
            return;
        }

        // Execute
        var sut = new EnigmaMachine(_testSettings);
        var firstOutput = sut.EncryptCharacter(inputChar);
        sut.Reset();
        var secondOutput = sut.EncryptCharacter(inputChar);

        // Assert
        Assert.That(firstOutput, Is.EqualTo(secondOutput));
    }

    [Test]
    public void UpdateSettingsGivesNewSettings()
    {
        // Arrange
        var dummySettings = new EnigmaSettings();

        if (_testSettings == null)
        {
            return;
        }

        // Execute
        var sut = new EnigmaMachine(dummySettings);
        sut.UpdateSettings(_testSettings);

        // Assert
        Assert.That(sut.Settings, Is.EqualTo(_testSettings));
        Assert.That(sut.Settings?.WheelSettings, Is.Not.Null);
    }

    private void CommonEncryptionTest(string input, string expectedOutput)
    {
        if (_testSettings == null)
        {
            return;
        }

        // Execute
        var sut = new EnigmaMachine(_testSettings);
        var output = sut.Encrypt(input);

        // Asser
        Assert.That(output, Is.EqualTo(expectedOutput));
    }

    private static EnigmaSettings? ReadTestSettings()
    {
        const string pathToTestFile = "./TestJsons/TestEnigmaSettings.json";

        var testSettingsAsString = File.ReadAllText(pathToTestFile);
        
        return JsonSerializer.Deserialize<EnigmaSettings>(testSettingsAsString);
    }
}