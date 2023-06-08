namespace EnigmaLibrary.Tests;

[TestFixture]
public class WheelTests
{
    private WheelSetting _testWheelSettings = new WheelSetting();

    [SetUp]
    public void Setup()
    {
        var testMappings = new int[26];

        for (var i = 0; i <= 25; i ++)
        {
            testMappings[i] = TestMapping(i);
        }

        _testWheelSettings.Mappings = testMappings;
    }

    [Test]
    public void MapsCorrectly()
    {
        // Arrange
        var testInput = 12;

        // Execute
        var sut = new Wheel(_testWheelSettings);
        var output = sut.Map(testInput);

        // Assert
        Assert.That(output, Is.EqualTo(TestMapping(testInput)));
    }

    [Test]
    public void ReverseMapsCorrect()
    {
        // Arrange
        var testInput = 12;

        // Execute
        var sut = new Wheel(_testWheelSettings);
        var output = sut.Map(testInput);
        output = sut.ReverseMap(output);

        // Assert
        Assert.That(output, Is.EqualTo(testInput));
    }

    [Test]
    public void ResetsBackToOriginalState()
    {
        // Arrange
        var testInput = 10;

        // Execute
        var sut = new Wheel(_testWheelSettings);
        sut.Rotate();
        sut.Reset();
        var output = sut.Map(testInput);

        // Assert
        Assert.That(output, Is.EqualTo(TestMapping(testInput)));
    }

    [Test]
    public void RotatesCorrectly()
    {
        // Arrange
        var testInput = 6;

        // Execute
        var sut = new Wheel(_testWheelSettings);
        sut.Rotate();
        var output = sut.Map(testInput);

        // Assert
        Assert.That(output, Is.EqualTo(TestMapping(testInput - 1)));
    }



    private static int TestMapping(int input)
    {
        return 25 - input;
    }
}