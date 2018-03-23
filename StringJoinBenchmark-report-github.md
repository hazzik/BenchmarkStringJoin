``` ini

BenchmarkDotNet=v0.10.11, OS=Windows 10 Redstone 3 [1709, Fall Creators Update] (10.0.16299.309)
Processor=Intel Core i7-2670QM CPU 2.20GHz (Sandy Bridge), ProcessorCount=8
Frequency=2143564 Hz, Resolution=466.5128 ns, Timer=TSC
.NET Core SDK=2.1.103
  [Host]     : .NET Core 2.0.6 (Framework 4.6.26212.01), 64bit RyuJIT
  DefaultJob : .NET Core 2.0.6 (Framework 4.6.26212.01), 64bit RyuJIT


```
|                        Method |     Mean |     Error |    StdDev | Scaled | ScaledSD |
|------------------------------ |---------:|----------:|----------:|-------:|---------:|
|              StringConcatJoin | 142.1 us | 2.7789 us | 4.0733 us |   1.00 |     0.00 |
|              StringFormatJoin | 141.0 us | 1.1955 us | 1.1183 us |   0.99 |     0.03 |
|             StringBuilderJoin | 137.0 us | 1.5032 us | 1.4061 us |   0.97 |     0.03 |
|           MyStringBuilderJoin | 130.5 us | 3.1192 us | 4.3727 us |   0.92 |     0.04 |
|   StringBuilderJoinEnumerable | 136.7 us | 1.8058 us | 1.5080 us |   0.96 |     0.03 |
| MyStringBuilderJoinEnumerable | 134.8 us | 0.3419 us | 0.2855 us |   0.95 |     0.03 |
