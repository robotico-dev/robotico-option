namespace Robotico.Option.Tests;

/// <summary>Extension methods: Tap, TapNone, RecoverWith, GetValueOrDefault.</summary>
public sealed class OptionExtensionsTests
{
    [Fact]
    public void Tap_Some_invokes_action()
    {
        int sideEffect = 0;
        Option<int> s = Option<int>.Some(11);
        Option<int> result = s.Tap(x => sideEffect = x);
        Assert.Equal(11, sideEffect);
        Assert.True(result.IsSome);
        Assert.Equal(11, result.GetValueOr(0));
    }

    [Fact]
    public void Tap_None_does_not_invoke_action()
    {
        int sideEffect = 0;
        Option<int> n = Option<int>.None;
        Option<int> result = n.Tap(_ => sideEffect = 1);
        Assert.Equal(0, sideEffect);
        Assert.True(result.IsNone);
    }

    [Fact]
    public void TapNone_None_invokes_action()
    {
        int sideEffect = 0;
        Option<int> n = Option<int>.None;
        Option<int> result = n.TapNone(() => sideEffect = 1);
        Assert.Equal(1, sideEffect);
        Assert.True(result.IsNone);
    }

    [Fact]
    public void TapNone_Some_does_not_invoke_action()
    {
        int sideEffect = 0;
        Option<int> s = Option<int>.Some(1);
        Option<int> result = s.TapNone(() => sideEffect = 1);
        Assert.Equal(0, sideEffect);
        Assert.True(result.IsSome);
    }

    [Fact]
    public void RecoverWith_Some_returns_this()
    {
        Option<int> s = Option<int>.Some(5);
        Option<int> fallback = Option<int>.Some(99);
        Option<int> result = s.RecoverWith(fallback);
        Assert.True(result.IsSome);
        Assert.Equal(5, result.GetValueOr(0));
    }

    [Fact]
    public void RecoverWith_None_returns_fallback()
    {
        Option<int> n = Option<int>.None;
        Option<int> fallback = Option<int>.Some(99);
        Option<int> result = n.RecoverWith(fallback);
        Assert.True(result.IsSome);
        Assert.Equal(99, result.GetValueOr(0));
    }

    [Fact]
    public void GetValueOrDefault_Some_returns_value()
    {
        Option<int> s = Option<int>.Some(42);
        Assert.Equal(42, s.GetValueOrDefault());
    }

    [Fact]
    public void GetValueOrDefault_None_returns_default()
    {
        Option<int> n = Option<int>.None;
        Assert.Equal(0, n.GetValueOrDefault());
    }
}
