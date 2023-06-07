namespace EnigmaLibrary.Tests;

[TestFixture]
public class CharExtensionTest
{
    [Test]
    public void LetterIsProperlyConvertedToNumber()
    {
        // Arrange
        const char input = 'A';

        // Execute
        var output = input.ToInteger();

        // Assert
        Assert.That(output, Is.EqualTo(0));
    }
}