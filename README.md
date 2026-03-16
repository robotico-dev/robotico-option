# Robotico.Option

Option (Maybe) type for .NET 8 and .NET 10. Immutable `readonly struct`, **zero package dependencies**. Use when a value may be absent; prefer over `T?` when you need explicit Match/Map/Bind and no null propagation.

[![.NET 8](https://img.shields.io/badge/.NET-8.0-512BD4?logo=dotnet)](https://dotnet.microsoft.com/download/dotnet/8.0)
[![.NET 10](https://img.shields.io/badge/.NET-10.0-512BD4?logo=dotnet)](https://dotnet.microsoft.com/download/dotnet/10.0)
[![C#](https://img.shields.io/badge/C%23-12-239120?logo=csharp)](https://learn.microsoft.com/en-us/dotnet/csharp/)
[![GitHub Packages](https://img.shields.io/badge/GitHub%20Packages-Robotico.Option-blue?logo=github)](https://github.com/robotico-dev/robotico-option/packages)
[![Build](https://github.com/robotico-dev/robotico-option/actions/workflows/publish.yml/badge.svg)](https://github.com/robotico-dev/robotico-option/actions/workflows/publish.yml)

## Features

- **Option&lt;T&gt;** — `readonly struct`; `None` (default) or `Some(value)`
- **Core API**: `Match`, `Map`, `Bind`, `GetValueOr`, `TryGetValue(out T)` (nullable flow)
- **Extensions**: `Tap`, `TapNone`, `RecoverWith`, `GetValueOrDefault`, `ToNullable`
- **Conversions**: `Option.FromNullable(T?)` (reference and value types); `ToNullable()` on Option
- **Async**: `MapAsync`, `BindAsync` on Option; `OptionTaskExtensions` for `Task<Option<T>>` (MapAsync, BindAsync, MatchAsync, TapAsync, TapNoneAsync)
- **Equality**: Structural equality; `None` equals default; `Some` equality by value

## Installation

```bash
dotnet add package Robotico.Option
```

## Quick start

```csharp
using Robotico.Option;

// None / Some
Option<int> none = Option<int>.None;
Option<int> some = Option<int>.Some(42);

// From nullable
Option<string> fromRef = Option.FromNullable((string?)null);  // None
Option<int> fromVal = Option.FromNullable((int?)42);            // Some(42)

// Map / Bind
Option<string> mapped = some.Map(x => x.ToString());
Option<int> bound = some.Bind(x => x > 0 ? Option<int>.Some(x) : Option<int>.None);

// Match
int result = opt.Match(x => x * 2, () => -1);

// Extensions
Option<int> withSideEffect = some.Tap(x => Console.WriteLine(x));
Option<int> fallback = none.RecoverWith(Option<int>.Some(0));
int? nullable = some.ToNullable();
```

## When to use

- **Option&lt;T&gt;** — When a value may be absent (e.g. dictionary lookup, optional config). Use `Match`/`Map`/`Bind` to handle both branches explicitly; avoids null propagation.

See the design doc (`docs/design.adoc`) for full guidance.

## Documentation

Full contract and design: see **`docs/design.adoc`**. Detailed design docs (AsciiDoc) are in the `docs/` folder:

- **Design** (`docs/design.adoc`) — When to use Option vs nullable, API naming, allocation.

**Public API:** This library uses `Microsoft.CodeAnalysis.PublicApiAnalyzers`. To populate `PublicAPI.Shipped.txt`, apply the code fix "Add to PublicAPI.Unshipped" (Fix All in project), then at release move entries to `PublicAPI.Shipped.txt`.

To build HTML from the AsciiDoc sources (e.g. with Asciidoctor):

```bash
asciidoctor docs/index.adoc -o docs/index.html
asciidoctor docs/design.adoc -o docs/design.html
```

## Versioning

We follow [Semantic Versioning](https://semver.org/). Version **1.0.0** is the first stable release. No breaking changes in minor/patch versions.

## Building, testing, and benchmarks

From the repo root:

```bash
dotnet restore
dotnet build -c Release
dotnet test tests/Robotico.Option.Tests/Robotico.Option.Tests.csproj -c Release
```

With coverage (Coverlet):

```bash
dotnet test tests/Robotico.Option.Tests/Robotico.Option.Tests.csproj -c Release --collect:"XPlat Code Coverage"
```

Optional CI gate (fail if line coverage below threshold):

```bash
dotnet test tests/Robotico.Option.Tests/Robotico.Option.Tests.csproj -c Release --collect:"XPlat Code Coverage" /p:CollectCoverage=true /p:Threshold=90 /p:ThresholdType=line
```

Run benchmarks (BenchmarkDotNet). **Recommended: run benchmarks in CI to catch performance regressions.**

```bash
dotnet run -c Release -p benchmarks/Robotico.Option.Benchmarks/Robotico.Option.Benchmarks.csproj -- --filter "*"
```

Or open the solution in your IDE and build from there.

## Analyzer suppressions (NoWarn)

The library intentionally suppresses the following analyzers; rationale is documented here for principal-grade transparency:

| Code | Rationale |
|------|------------|
| **CA1716** | Identifier naming rule; suppressed at project level for consistency with shared Robotico build rules. |
| **CA1724** | Type name `Option` matches namespace `Robotico.Option`; suppressed on the type with `SuppressMessage`. "Option" is the standard domain term; renaming would harm discoverability. |
| **CA1000** | Static members on generic type: `Option.Some`, `Option.None`, `Option.FromNullable` are the intended API; the analyzer discourages static members on generics, but this pattern is standard for Option/Maybe types. |

All other warnings are treated as errors. See `Directory.Build.props` and `Robotico.Option.csproj` for the full build bar.

## License

See repository license file.
