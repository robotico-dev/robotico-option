using Robotico.Option;
using Xunit;

namespace Robotico.Option.Tests;

public sealed class OptionTests
{
    [Fact]
    public void None_has_IsNone_true_and_IsSome_false()
    {
        Option<int> n = Option<int>.None;
        Assert.True(n.IsNone);
        Assert.False(n.IsSome);
    }

    [Fact]
    public void Some_has_IsSome_true_and_GetValueOr_returns_value()
    {
        Option<int> s = Option<int>.Some(42);
        Assert.True(s.IsSome);
        Assert.False(s.IsNone);
        Assert.Equal(42, s.GetValueOr(0));
    }

    [Fact]
    public void Match_Some_invokes_some_branch()
    {
        Option<int> s = Option<int>.Some(3);
        int r = s.Match(x => x * 2, () => -1);
        Assert.Equal(6, r);
    }

    [Fact]
    public void Map_Some_maps_value()
    {
        Option<int> s = Option<int>.Some(5);
        Option<string> mapped = s.Map(x => x.ToString(System.Globalization.CultureInfo.InvariantCulture));
        Assert.True(mapped.IsSome);
        Assert.Equal("5", mapped.GetValueOr(string.Empty));
    }
}
