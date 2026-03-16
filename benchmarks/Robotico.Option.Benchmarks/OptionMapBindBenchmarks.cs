using System.Globalization;
using BenchmarkDotNet.Attributes;
using Robotico.Option;

namespace Robotico.Option.Benchmarks;

[MemoryDiagnoser]
[ShortRunJob]
public class OptionMapBindBenchmarks
{
    private static readonly Option<int> Some42 = Option<int>.Some(42);

    [Benchmark(Baseline = true)]
    public Option<int> Some_Map()
    {
        return Some42.Map(x => x + 1);
    }

    [Benchmark]
    public Option<string> Some_Map_ToString()
    {
        return Some42.Map(x => x.ToString(CultureInfo.InvariantCulture));
    }

    [Benchmark]
    public Option<int> Some_Bind()
    {
        return Some42.Bind(x => Option<int>.Some(x + 1));
    }

    [Benchmark]
    public Option<int> None_Map_Remains_None()
    {
        Option<int> n = Option<int>.None;
        return n.Map(x => x + 1);
    }

    [Benchmark]
    public int Some_Match()
    {
        return Some42.Match(v => v, () => 0);
    }

    [Benchmark]
    public int Some_GetValueOr()
    {
        return Some42.GetValueOr(0);
    }

    [Benchmark]
    public int? Some_GetValueOrDefault()
    {
        return Some42.GetValueOrDefault();
    }
}
