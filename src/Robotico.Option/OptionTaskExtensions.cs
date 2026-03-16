namespace Robotico.Option;

/// <summary>Task extensions for Option: MapAsync, BindAsync, MatchAsync, TapAsync, TapNoneAsync.</summary>
public static class OptionTaskExtensions
{
    /// <summary>Maps a task of Option to Option of mapped value.</summary>
    public static async Task<Option<TMapped>> MapAsync<T, TMapped>(this Task<Option<T>> task, Func<T, TMapped> mapping)
    {
        ArgumentNullException.ThrowIfNull(task);
        ArgumentNullException.ThrowIfNull(mapping);
        Option<T> option = await task.ConfigureAwait(false);
        return option.Map(mapping);
    }

    /// <summary>Maps a task of Option using an async mapping.</summary>
    public static async Task<Option<TMapped>> MapAsync<T, TMapped>(this Task<Option<T>> task, Func<T, Task<TMapped>> mapping)
    {
        ArgumentNullException.ThrowIfNull(task);
        ArgumentNullException.ThrowIfNull(mapping);
        Option<T> option = await task.ConfigureAwait(false);
        return await option.MapAsync(mapping).ConfigureAwait(false);
    }

    /// <summary>Binds a task of Option.</summary>
    public static async Task<Option<TMapped>> BindAsync<T, TMapped>(this Task<Option<T>> task, Func<T, Option<TMapped>> binding)
    {
        ArgumentNullException.ThrowIfNull(task);
        ArgumentNullException.ThrowIfNull(binding);
        Option<T> option = await task.ConfigureAwait(false);
        return option.Bind(binding);
    }

    /// <summary>Binds a task of Option with async binding.</summary>
    public static async Task<Option<TMapped>> BindAsync<T, TMapped>(this Task<Option<T>> task, Func<T, Task<Option<TMapped>>> binding)
    {
        ArgumentNullException.ThrowIfNull(task);
        ArgumentNullException.ThrowIfNull(binding);
        Option<T> option = await task.ConfigureAwait(false);
        return await option.BindAsync(binding).ConfigureAwait(false);
    }

    /// <summary>Pattern match on a task of Option.</summary>
    public static async Task<TResult> MatchAsync<T, TResult>(this Task<Option<T>> task, Func<T, TResult> some, Func<TResult> none)
    {
        ArgumentNullException.ThrowIfNull(task);
        ArgumentNullException.ThrowIfNull(some);
        ArgumentNullException.ThrowIfNull(none);
        Option<T> option = await task.ConfigureAwait(false);
        return option.Match(some, none);
    }

    /// <summary>Runs a side effect when Some on a task of Option; returns the same task result.</summary>
    public static async Task<Option<T>> TapAsync<T>(this Task<Option<T>> task, Action<T> action)
    {
        ArgumentNullException.ThrowIfNull(task);
        ArgumentNullException.ThrowIfNull(action);
        Option<T> option = await task.ConfigureAwait(false);
        return option.Tap(action);
    }

    /// <summary>Runs a side effect when None on a task of Option; returns the same task result.</summary>
    public static async Task<Option<T>> TapNoneAsync<T>(this Task<Option<T>> task, Action action)
    {
        ArgumentNullException.ThrowIfNull(task);
        ArgumentNullException.ThrowIfNull(action);
        Option<T> option = await task.ConfigureAwait(false);
        return option.TapNone(action);
    }
}
