namespace Robotico.Option.Tests;

/// <summary>Option.FromNullable and ToNullable: reference and value types.</summary>
public sealed class OptionConversionsTests
{
    [Fact]
    public void FromNullable_reference_null_returns_None()
    {
        string? nil = null;
        Option<string> o = Option.FromNullable(nil);
        Assert.True(o.IsNone);
    }

    [Fact]
    public void FromNullable_reference_value_returns_Some()
    {
        string value = "hello";
        Option<string> o = Option.FromNullable(value);
        Assert.True(o.IsSome);
        Assert.Equal("hello", o.GetValueOr(string.Empty));
    }

    [Fact]
    public void FromNullable_struct_null_returns_None()
    {
        int? nil = null;
        Option<int> o = Option.FromNullable(nil);
        Assert.True(o.IsNone);
    }

    [Fact]
    public void FromNullable_struct_value_returns_Some()
    {
        int? value = 42;
        Option<int> o = Option.FromNullable(value);
        Assert.True(o.IsSome);
        Assert.Equal(42, o.GetValueOr(0));
    }

    [Fact]
    public void ToNullable_class_Some_returns_value()
    {
        Option<string> s = Option<string>.Some("x");
        Assert.Equal("x", s.ToNullable());
    }

    [Fact]
    public void ToNullable_class_None_returns_null()
    {
        Option<string> n = Option<string>.None;
        Assert.Null(n.ToNullable());
    }

    [Fact]
    public void ToNullable_struct_Some_returns_value()
    {
        Option<int> s = Option<int>.Some(7);
        int? result = s.ToNullable();
        Assert.True(result.HasValue);
        Assert.Equal(7, result!.Value);
    }
}
