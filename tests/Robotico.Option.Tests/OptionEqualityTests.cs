namespace Robotico.Option.Tests;

/// <summary>Equality and GetHashCode for Option: None, Some, operators.</summary>
public sealed class OptionEqualityTests
{
    [Fact]
    public void None_equality_same_as_default()
    {
        Option<int> a = Option<int>.None;
        Option<int> b = default;
        Assert.True(a.Equals(b));
        Assert.True(a == b);
        Assert.False(a != b);
        Assert.Equal(a.GetHashCode(), b.GetHashCode());
    }

    [Fact]
    public void Some_equality_based_on_value()
    {
        Option<int> a = Option<int>.Some(7);
        Option<int> b = Option<int>.Some(7);
        Option<int> c = Option<int>.Some(8);
        Assert.True(a.Equals(b));
        Assert.True(a == b);
        Assert.False(a != b);
        Assert.Equal(a.GetHashCode(), b.GetHashCode());
        Assert.False(a.Equals(c));
        Assert.True(a != c);
    }
}
