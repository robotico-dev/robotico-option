using System.Globalization;

namespace Robotico.Option.Tests;

/// <summary>Task extension methods for Option: MapAsync, BindAsync, MatchAsync, TapAsync, TapNoneAsync.</summary>
public sealed class OptionTaskExtensionsTests
{
    [Fact]
    public async Task MapAsync_Some_maps_value()
    {
        Task<Option<int>> task = Task.FromResult(Option<int>.Some(5));
        Option<string> result = await task.MapAsync(x => x.ToString(CultureInfo.InvariantCulture));
        Assert.True(result.IsSome);
        Assert.Equal("5", result.GetValueOr(string.Empty));
    }

    [Fact]
    public async Task MapAsync_None_remains_None()
    {
        Task<Option<int>> task = Task.FromResult(Option<int>.None);
        Option<string> result = await task.MapAsync(x => x.ToString(CultureInfo.InvariantCulture));
        Assert.True(result.IsNone);
    }

    [Fact]
    public async Task MapAsync_async_delegate_Some_maps_value()
    {
        Task<Option<int>> task = Task.FromResult(Option<int>.Some(5));
        Option<string> result = await task.MapAsync(async x =>
        {
            await Task.Yield();
            return x.ToString(CultureInfo.InvariantCulture);
        });
        Assert.True(result.IsSome);
        Assert.Equal("5", result.GetValueOr(string.Empty));
    }

    [Fact]
    public async Task BindAsync_Some_to_Some_returns_inner()
    {
        Task<Option<int>> task = Task.FromResult(Option<int>.Some(2));
        Option<int> result = await task.BindAsync(x => Option<int>.Some(x + 1));
        Assert.True(result.IsSome);
        Assert.Equal(3, result.GetValueOr(0));
    }

    [Fact]
    public async Task BindAsync_None_remains_None()
    {
        Task<Option<int>> task = Task.FromResult(Option<int>.None);
        Option<int> result = await task.BindAsync(x => Option<int>.Some(x));
        Assert.True(result.IsNone);
    }

    [Fact]
    public async Task BindAsync_async_delegate_Some_returns_bound()
    {
        Task<Option<int>> task = Task.FromResult(Option<int>.Some(3));
        Option<int> result = await task.BindAsync(async x =>
        {
            await Task.Yield();
            return Option<int>.Some(x * 2);
        });
        Assert.True(result.IsSome);
        Assert.Equal(6, result.GetValueOr(0));
    }

    [Fact]
    public async Task MatchAsync_Some_invokes_some_branch()
    {
        Task<Option<int>> task = Task.FromResult(Option<int>.Some(7));
        int r = await task.MatchAsync(x => x + 1, () => -1);
        Assert.Equal(8, r);
    }

    [Fact]
    public async Task MatchAsync_None_invokes_none_branch()
    {
        Task<Option<int>> task = Task.FromResult(Option<int>.None);
        int r = await task.MatchAsync(x => x, () => 99);
        Assert.Equal(99, r);
    }

    [Fact]
    public async Task TapAsync_Some_invokes_action()
    {
        int sideEffect = 0;
        Task<Option<int>> task = Task.FromResult(Option<int>.Some(11));
        Option<int> result = await task.TapAsync(x => sideEffect = x);
        Assert.Equal(11, sideEffect);
        Assert.True(result.IsSome);
    }

    [Fact]
    public async Task TapNoneAsync_None_invokes_action()
    {
        int sideEffect = 0;
        Task<Option<int>> task = Task.FromResult(Option<int>.None);
        Option<int> result = await task.TapNoneAsync(() => sideEffect = 1);
        Assert.Equal(1, sideEffect);
        Assert.True(result.IsNone);
    }
}
