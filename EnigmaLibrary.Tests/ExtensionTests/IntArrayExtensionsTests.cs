namespace EnigmaLibrary.Tests;

[TestFixture]
public class IntArrayExtensionsTests
{
    [Test]
    public void IntArrayIsProperlyConvertedToString()
    {
        // Arrange
        var input = new[] {0, 1, 2, 3};

        // Execute
        var output = input.ArrayToString();

        // Assert
        Assert.That(output, Is.EqualTo("ABCD"));
    }
}