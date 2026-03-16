namespace Robotico.Option;

/// <summary>
/// Extension methods for Option: Tap, TapNone, RecoverWith, GetValueOrDefault, ToNullable.
/// </summary>
public static class OptionExtensions
{
    /// <summary>Runs a side effect when Some; returns the same option.</summary>
    public static Option<T> Tap<T>(this Option<T> option, Action<T> action)
    {
        ArgumentNullException.ThrowIfNull(action);
        if (option.TryGetValue(out T? value))
        {
            action(value!);
        }
        return option;
    }

    /// <summary>Runs a side effect when None; returns the same option.</summary>
    public static Option<T> TapNone<T>(this Option<T> option, Action action)
    {
        ArgumentNullException.ThrowIfNull(action);
        if (option.IsNone)
        {
            action();
        }
        return option;
    }

    /// <summary>Returns this option if Some; otherwise returns the fallback option.</summary>
    public static Option<T> RecoverWith<T>(this Option<T> option, Option<T> fallback) => option.IsSome ? option : fallback;

    /// <summary>Returns the value if Some; otherwise default (null for reference types, default for value types).</summary>
    public static T? GetValueOrDefault<T>(this Option<T> option) => option.TryGetValue(out T? value) ? value : default;

    /// <summary>Converts to nullable: Some(value) → value, None → null. For reference types T? is nullable reference; for value types T? is Nullable&lt;T&gt;.</summary>
    public static T? ToNullable<T>(this Option<T> option) => option.Match(v => (T?)v, () => default(T?));
}
