using System.Globalization;

namespace Robotico.Option.Tests;

/// <summary>Basic Option state and core API: None/Some, Match, Map, Bind, GetValueOr.</summary>
public sealed class OptionBasicsTests
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
    public void TryGetValue_returns_true_and_sets_value_when_Some()
    {
        Option<int> s = Option<int>.Some(42);
        Assert.True(s.TryGetValue(out int value));
        Assert.Equal(42, value);
    }

    [Fact]
    public void TryGetValue_returns_false_when_None()
    {
        Option<int> n = Option<int>.None;
        Assert.False(n.TryGetValue(out int value));
        Assert.Equal(0, value);
    }

    [Fact]
    public void Match_Some_invokes_some_branch()
    {
        Option<int> s = Option<int>.Some(3);
        int r = s.Match(x => x * 2, () => -1);
        Assert.Equal(6, r);
    }

    [Fact]
    public void Match_None_invokes_none_branch()
    {
        Option<int> n = Option<int>.None;
        int r = n.Match(x => x * 2, () => -1);
        Assert.Equal(-1, r);
    }

    [Fact]
    public void Map_Some_maps_value()
    {
        Option<int> s = Option<int>.Some(5);
        Option<string> mapped = s.Map(x => x.ToString(System.Globalization.CultureInfo.InvariantCulture));
        Assert.True(mapped.IsSome);
        Assert.Equal("5", mapped.GetValueOr(string.Empty));
    }

    [Fact]
    public void Map_None_remains_None()
    {
        Option<int> n = Option<int>.None;
        Option<string> mapped = n.Map(x => x.ToString(CultureInfo.InvariantCulture));
        Assert.True(mapped.IsNone);
    }

    [Fact]
    public void Bind_Some_to_Some_returns_inner_value()
    {
        Option<int> s = Option<int>.Some(2);
        Option<int> bound = s.Bind(x => Option<int>.Some(x + 1));
        Assert.True(bound.IsSome);
        Assert.Equal(3, bound.GetValueOr(0));
    }

    [Fact]
    public void Bind_Some_to_None_returns_None()
    {
        Option<int> s = Option<int>.Some(2);
        Option<int> bound = s.Bind(_ => Option<int>.None);
        Assert.True(bound.IsNone);
    }

    [Fact]
    public void Bind_None_remains_None()
    {
        Option<int> n = Option<int>.None;
        Option<string> bound = n.Bind(x => Option<string>.Some(x.ToString(CultureInfo.InvariantCulture)));
        Assert.True(bound.IsNone);
    }

    [Fact]
    public void GetValueOr_None_returns_defaultValue()
    {
        Option<int> n = Option<int>.None;
        Assert.Equal(99, n.GetValueOr(99));
    }
}
