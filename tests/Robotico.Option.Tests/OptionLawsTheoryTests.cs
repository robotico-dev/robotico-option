namespace Robotico.Option.Tests;

/// <summary>Property-style and parameterized tests for Option laws using [Theory] and [InlineData].</summary>
public sealed class OptionLawsTheoryTests
{
    [Theory]
    [InlineData(42)]
    [InlineData(0)]
    [InlineData(-1)]
    public void Map_identity_Some_preserves_value(int value)
    {
        Option<int> s = Option<int>.Some(value);
        Option<int> mapped = s.Map(x => x);
        Assert.True(mapped.IsSome);
        Assert.Equal(value, mapped.GetValueOr(0));
    }

    [Theory]
    [InlineData(1, 2, 3)]
    [InlineData(10, 20, 30)]
    public void Bind_Some_identity_preserves_value(int a, int b, int expectedSum)
    {
        Option<int> s = Option<int>.Some(a);
        Option<int> bound = s.Bind(x => Option<int>.Some(x + b));
        Assert.True(bound.IsSome);
        Assert.Equal(expectedSum, bound.GetValueOr(0));
    }

    [Fact]
    public void Map_None_identity_remains_None()
    {
        Option<int> n = Option<int>.None;
        Option<int> mapped = n.Map(x => x);
        Assert.True(mapped.IsNone);
    }

    [Fact]
    public void Bind_None_identity_remains_None()
    {
        Option<int> n = Option<int>.None;
        Option<int> bound = n.Bind(x => Option<int>.Some(x));
        Assert.True(bound.IsNone);
    }
}
