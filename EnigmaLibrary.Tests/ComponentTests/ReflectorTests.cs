namespace EnigmaLibrary.Tests;

[TestFixture]
public class ReflectorTests
{
    private readonly Random _rand = new Random();

    [Test]
    public void MapsCorrectly()
    {
        // Arrange
        var min = 0;
        var max = 25;
        var input = _rand.Next(min, max + 1);

        var sut = new Reflector();

        // Execute
        var output = sut.Map(input);

        // Assert
        Assert.That(output, Is.EqualTo(max - input));
    }
}