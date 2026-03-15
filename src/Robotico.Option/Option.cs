namespace Robotico.Option;

/// <summary>
/// Option (Maybe) type: either Some(value) or None. Value type (struct).
/// </summary>
/// <typeparam name="T">The type of the optional value.</typeparam>
public readonly struct Option<T> : IEquatable<Option<T>>
{
    private readonly T? _value;
    private readonly bool _isSome;

    private Option(T? value, bool isSome)
    {
        _value = value;
        _isSome = isSome;
    }

    /// <summary>
    /// None (no value).
    /// </summary>
    public static Option<T> None => default;

    /// <summary>
    /// Some(value). Use for reference types and nullable value types; for value types consider passing explicitly.
    /// </summary>
    public static Option<T> Some(T value) => new(value, true);

    /// <summary>
    /// True if this option has a value.
    /// </summary>
    public bool IsSome => _isSome;

    /// <summary>
    /// True if this option has no value.
    /// </summary>
    public bool IsNone => !_isSome;

    /// <summary>
    /// Pattern match: run <paramref name="some"/> if Some, else <paramref name="none"/>.
    /// </summary>
    public TResult Match<TResult>(Func<T, TResult> some, Func<TResult> none)
    {
        ArgumentNullException.ThrowIfNull(some);
        ArgumentNullException.ThrowIfNull(none);
        return _isSome ? some(_value!) : none();
    }

    /// <summary>
    /// Map the value if Some; otherwise remain None.
    /// </summary>
    public Option<TMapped> Map<TMapped>(Func<T, TMapped> mapping)
    {
        ArgumentNullException.ThrowIfNull(mapping);
        return _isSome ? Option<TMapped>.Some(mapping(_value!)) : Option<TMapped>.None;
    }

    /// <summary>
    /// Bind (flatMap): if Some, apply <paramref name="binding"/>; otherwise remain None.
    /// </summary>
    public Option<TMapped> Bind<TMapped>(Func<T, Option<TMapped>> binding)
    {
        ArgumentNullException.ThrowIfNull(binding);
        return _isSome ? binding(_value!) : Option<TMapped>.None;
    }

    /// <summary>
    /// Returns the value if Some; otherwise <paramref name="defaultValue"/>.
    /// </summary>
    public T GetValueOr(T defaultValue) => _isSome ? _value! : defaultValue;

    /// <inheritdoc />
    public bool Equals(Option<T> other) =>
        _isSome == other._isSome && (!_isSome || EqualityComparer<T>.Default.Equals(_value, other._value));

    /// <inheritdoc />
    public override bool Equals(object? obj) => obj is Option<T> other && Equals(other);

    /// <inheritdoc />
    public override int GetHashCode() => _isSome && _value is not null ? _value.GetHashCode() : 0;

    /// <summary>Equality operator.</summary>
    public static bool operator ==(Option<T> left, Option<T> right) => left.Equals(right);

    /// <summary>Inequality operator.</summary>
    public static bool operator !=(Option<T> left, Option<T> right) => !left.Equals(right);
}
