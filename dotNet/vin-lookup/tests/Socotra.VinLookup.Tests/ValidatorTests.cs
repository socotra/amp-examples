namespace Socotra.VinLookup.Tests;

public class ValidatorTests
{
    [Fact]
    public void HasItems_WhenArrayHasItems_ReturnsTrue()
    {
        // Arrange 
        var _sut = new Validator();
        var arr = new[] { "foo", "bar" };
        // Act 
        var actualResult = _sut.HasItems(arr);
        // Assert
        Assert.True(actualResult);
    }

}