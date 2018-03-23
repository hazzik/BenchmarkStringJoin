using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

public class StringJoinBenchmark
{
    private const int N = 10000;
    private readonly byte[] data;


    public StringJoinBenchmark()
    {
        data = new byte[N];
        new Random(42).NextBytes(data);
    }

    [Benchmark(Baseline =  true)]
    public string StringConcatJoin() => "[" + string.Join(",", data) + "]";
    
    [Benchmark]
    public string StringFormatJoin() => $"[{string.Join(",", data)}]";

    [Benchmark]
    public string StringBuilderJoin() => new StringBuilder().Append("[").AppendJoin(",", data).Append("]").ToString();

    [Benchmark]
    public string MyStringBuilderJoin() => StringBuilderExtensions.AppendJoin(new StringBuilder().Append("["), ",", data).Append("]").ToString();
}

internal static class StringBuilderExtensions
{
    public static StringBuilder AppendJoin<T>(this StringBuilder sb, string separator, params T[] values)
    {
        if (values.Length > 0)
        {
            sb.EnsureCapacity(sb.Capacity + separator.Length * values.Length);

            sb.Append(values[0]);

            for (var i = 1; i < values.Length; i++)
            {
                sb.Append(separator).Append(values[i]);
            }
        }

        return sb;
    }

    public static StringBuilder AppendJoin(this StringBuilder sb, string separator, params string[] values)
    {
        if (values.Length > 0)
        {
            sb.EnsureCapacity(sb.Capacity + separator.Length * (values.Length - 1));

            sb.Append(values[0]);

            for (var i = 1; i < values.Length; i++)
            {
                sb.Append(separator);
                sb.Append(values[i]);
            }
        }

        return sb;
    }
   
    public static StringBuilder AppendJoin<T>(this StringBuilder sb, string separator,  IEnumerable<T> values)
    {
        using (var enumerator = values.GetEnumerator())
        {
            if (enumerator.MoveNext())
            {
                sb.Append(enumerator.Current);
            }

            while (enumerator.MoveNext())
            {
                sb.Append(separator);
                sb.Append(enumerator.Current);
            }
        }

        return sb;
    }

    public static StringBuilder AppendJoin<T>(this StringBuilder sb, char separator, params T[] values)
        => sb.AppendJoin(separator.ToString(), values);

    public static StringBuilder AppendJoin<T>(this StringBuilder sb, char separator, IEnumerable<T> values)
        => sb.AppendJoin(separator.ToString(), values);

    public static StringBuilder AppendJoin(this StringBuilder sb, char separator, params string[] values)
        => sb.AppendJoin(separator.ToString(), values);
}

public class Program
{
    public static void Main(string[] args)
    {
        var summary = BenchmarkRunner.Run<StringJoinBenchmark>();
    }
}