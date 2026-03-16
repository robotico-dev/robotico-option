namespace Robotico.Option;

/// <summary>Factory and conversion helpers for Option.</summary>
public static class Option
{
    /// <summary>Creates an Option from a nullable reference: null → None, value → Some(value).</summary>
    public static Option<T> FromNullable<T>(T? value)
        where T : class =>
        value is null ? Option<T>.None : Option<T>.Some(value);

    /// <summary>Creates an Option from a nullable value type: null → None, value → Some(value).</summary>
    public static Option<T> FromNullable<T>(T? value)
        where T : struct =>
        value.HasValue ? Option<T>.Some(value.Value) : Option<T>.None;
}
