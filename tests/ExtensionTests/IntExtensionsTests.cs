namespace EnigmaLibrary.Tests;

[TestFixture]
public class IntExtensionsTests
{
    [Test]
    public void IntIsCorrectlyConvertedToCharacter()
    {
        // Arrange
        const int input = 0;
        const char expectedOutput = 'A';

        //Execute
        var output = input.ToCharacter();

        // Assert
        Assert.That(output, Is.EqualTo(expectedOutput));
    }
}