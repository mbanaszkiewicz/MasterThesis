``` ini

BenchmarkDotNet=v0.13.0, OS=Windows 10.0.19042.985 (20H2/October2020Update)
AMD Ryzen 5 3600, 1 CPU, 12 logical and 6 physical cores
  [Host]               : .NET Framework 4.8 (4.8.4341.0), X64 RyuJIT
  .NET Framework 4.7.2 : .NET Framework 4.8 (4.8.4341.0), X64 RyuJIT


```
|                               Method |                  Job |              Runtime |        N |             Mean |           Error |           StdDev |        Gen 0 |        Gen 1 |      Gen 2 |        Allocated |
|------------------------------------- |--------------------- |--------------------- |--------- |-----------------:|----------------:|-----------------:|-------------:|-------------:|-----------:|-----------------:|
|        **SequentialFunctionalQuickSort** |        **.NET Core 3.1** |        **.NET Core 3.1** |    **10000** |               **NA** |              **NA** |               **NA** |            **-** |            **-** |          **-** |                **-** |
|          ParallelFunctionalQuickSort |        .NET Core 3.1 |        .NET Core 3.1 |    10000 |               NA |              NA |               NA |            - |            - |          - |                - |
| OptimizedParallelFunctionalQuickSort |        .NET Core 3.1 |        .NET Core 3.1 |    10000 |               NA |              NA |               NA |            - |            - |          - |                - |
|        SequentialImperativeQuickSort |        .NET Core 3.1 |        .NET Core 3.1 |    10000 |               NA |              NA |               NA |            - |            - |          - |                - |
|          ParallelImperativeQuickSort |        .NET Core 3.1 |        .NET Core 3.1 |    10000 |               NA |              NA |               NA |            - |            - |          - |                - |
| OptimizedParallelImperativeQuickSort |        .NET Core 3.1 |        .NET Core 3.1 |    10000 |               NA |              NA |               NA |            - |            - |          - |                - |
|        SequentialFunctionalQuickSort | .NET Framework 4.7.2 | .NET Framework 4.7.2 |    10000 |      32,984.9 μs |       605.62 μs |        868.57 μs |    2937.5000 |     625.0000 |   312.5000 |     17,675,968 B |
|          ParallelFunctionalQuickSort | .NET Framework 4.7.2 | .NET Framework 4.7.2 |    10000 |      89,309.1 μs |     1,561.47 μs |      1,384.20 μs |    4500.0000 |     500.0000 |   166.6667 |     26,553,197 B |
| OptimizedParallelFunctionalQuickSort | .NET Framework 4.7.2 | .NET Framework 4.7.2 |    10000 |      23,614.3 μs |       466.01 μs |        930.68 μs |    4000.0000 |    1000.0000 |          - |     22,615,680 B |
|        SequentialImperativeQuickSort | .NET Framework 4.7.2 | .NET Framework 4.7.2 |    10000 |         876.6 μs |        15.23 μs |         13.51 μs |      83.0078 |      41.0156 |          - |        521,595 B |
|          ParallelImperativeQuickSort | .NET Framework 4.7.2 | .NET Framework 4.7.2 |    10000 |      51,871.8 μs |       989.77 μs |      1,059.05 μs |    2200.0000 |     200.0000 |          - |      8,526,438 B |
| OptimizedParallelImperativeQuickSort | .NET Framework 4.7.2 | .NET Framework 4.7.2 |    10000 |       1,989.0 μs |        38.83 μs |         47.69 μs |     410.1563 |     203.1250 |          - |      2,568,062 B |
|        **SequentialFunctionalQuickSort** |        **.NET Core 3.1** |        **.NET Core 3.1** |   **100000** |               **NA** |              **NA** |               **NA** |            **-** |            **-** |          **-** |                **-** |
|          ParallelFunctionalQuickSort |        .NET Core 3.1 |        .NET Core 3.1 |   100000 |               NA |              NA |               NA |            - |            - |          - |                - |
| OptimizedParallelFunctionalQuickSort |        .NET Core 3.1 |        .NET Core 3.1 |   100000 |               NA |              NA |               NA |            - |            - |          - |                - |
|        SequentialImperativeQuickSort |        .NET Core 3.1 |        .NET Core 3.1 |   100000 |               NA |              NA |               NA |            - |            - |          - |                - |
|          ParallelImperativeQuickSort |        .NET Core 3.1 |        .NET Core 3.1 |   100000 |               NA |              NA |               NA |            - |            - |          - |                - |
| OptimizedParallelImperativeQuickSort |        .NET Core 3.1 |        .NET Core 3.1 |   100000 |               NA |              NA |               NA |            - |            - |          - |                - |
|        SequentialFunctionalQuickSort | .NET Framework 4.7.2 | .NET Framework 4.7.2 |   100000 |     453,121.0 μs |     8,697.94 μs |      9,306.69 μs |   35000.0000 |    8000.0000 |  2000.0000 |    205,550,544 B |
|          ParallelFunctionalQuickSort | .NET Framework 4.7.2 | .NET Framework 4.7.2 |   100000 |     987,531.4 μs |    18,253.37 μs |     16,181.14 μs |   52000.0000 |   11000.0000 |  2000.0000 |    294,496,224 B |
| OptimizedParallelFunctionalQuickSort | .NET Framework 4.7.2 | .NET Framework 4.7.2 |   100000 |     317,582.3 μs |     6,348.45 μs |     18,316.74 μs |   40000.0000 |   10000.0000 |  1000.0000 |    247,586,368 B |
|        SequentialImperativeQuickSort | .NET Framework 4.7.2 | .NET Framework 4.7.2 |   100000 |      14,839.5 μs |       295.17 μs |        362.49 μs |     843.7500 |     375.0000 |   140.6250 |      5,214,412 B |
|          ParallelImperativeQuickSort | .NET Framework 4.7.2 | .NET Framework 4.7.2 |   100000 |     520,526.3 μs |    10,140.27 μs |     10,413.31 μs |   50000.0000 |    1000.0000 |          - |     86,386,160 B |
| OptimizedParallelImperativeQuickSort | .NET Framework 4.7.2 | .NET Framework 4.7.2 |   100000 |      23,543.2 μs |       469.41 μs |        716.84 μs |    6000.0000 |     718.7500 |   218.7500 |     19,195,859 B |
|        **SequentialFunctionalQuickSort** |        **.NET Core 3.1** |        **.NET Core 3.1** |  **1000000** |               **NA** |              **NA** |               **NA** |            **-** |            **-** |          **-** |                **-** |
|          ParallelFunctionalQuickSort |        .NET Core 3.1 |        .NET Core 3.1 |  1000000 |               NA |              NA |               NA |            - |            - |          - |                - |
| OptimizedParallelFunctionalQuickSort |        .NET Core 3.1 |        .NET Core 3.1 |  1000000 |               NA |              NA |               NA |            - |            - |          - |                - |
|        SequentialImperativeQuickSort |        .NET Core 3.1 |        .NET Core 3.1 |  1000000 |               NA |              NA |               NA |            - |            - |          - |                - |
|          ParallelImperativeQuickSort |        .NET Core 3.1 |        .NET Core 3.1 |  1000000 |               NA |              NA |               NA |            - |            - |          - |                - |
| OptimizedParallelImperativeQuickSort |        .NET Core 3.1 |        .NET Core 3.1 |  1000000 |               NA |              NA |               NA |            - |            - |          - |                - |
|        SequentialFunctionalQuickSort | .NET Framework 4.7.2 | .NET Framework 4.7.2 |  1000000 |   5,519,318.0 μs |    79,943.79 μs |     74,779.47 μs |  366000.0000 |   76000.0000 |  6000.0000 |  2,322,463,384 B |
|          ParallelFunctionalQuickSort | .NET Framework 4.7.2 | .NET Framework 4.7.2 |  1000000 |  10,967,941.6 μs |   192,665.48 μs |    206,149.91 μs |  529000.0000 |   93000.0000 |  4000.0000 |  3,210,781,856 B |
| OptimizedParallelFunctionalQuickSort | .NET Framework 4.7.2 | .NET Framework 4.7.2 |  1000000 |   4,056,224.0 μs |    79,738.79 μs |    143,785.49 μs |  426000.0000 |   98000.0000 |  3000.0000 |  2,729,082,816 B |
|        SequentialImperativeQuickSort | .NET Framework 4.7.2 | .NET Framework 4.7.2 |  1000000 |     217,608.7 μs |     1,314.75 μs |      1,097.87 μs |    8666.6667 |    3666.6667 |  1000.0000 |     52,144,051 B |
|          ParallelImperativeQuickSort | .NET Framework 4.7.2 | .NET Framework 4.7.2 |  1000000 |   5,059,685.6 μs |    94,854.78 μs |     93,160.08 μs |  496000.0000 |    3000.0000 |          - |    862,896,312 B |
| OptimizedParallelImperativeQuickSort | .NET Framework 4.7.2 | .NET Framework 4.7.2 |  1000000 |     238,602.1 μs |     4,771.93 μs |     12,232.28 μs |   66666.6667 |    4333.3333 |  1000.0000 |    152,776,928 B |
|        **SequentialFunctionalQuickSort** |        **.NET Core 3.1** |        **.NET Core 3.1** | **10000000** |               **NA** |              **NA** |               **NA** |            **-** |            **-** |          **-** |                **-** |
|          ParallelFunctionalQuickSort |        .NET Core 3.1 |        .NET Core 3.1 | 10000000 |               NA |              NA |               NA |            - |            - |          - |                - |
| OptimizedParallelFunctionalQuickSort |        .NET Core 3.1 |        .NET Core 3.1 | 10000000 |               NA |              NA |               NA |            - |            - |          - |                - |
|        SequentialImperativeQuickSort |        .NET Core 3.1 |        .NET Core 3.1 | 10000000 |               NA |              NA |               NA |            - |            - |          - |                - |
|          ParallelImperativeQuickSort |        .NET Core 3.1 |        .NET Core 3.1 | 10000000 |               NA |              NA |               NA |            - |            - |          - |                - |
| OptimizedParallelImperativeQuickSort |        .NET Core 3.1 |        .NET Core 3.1 | 10000000 |               NA |              NA |               NA |            - |            - |          - |                - |
|        SequentialFunctionalQuickSort | .NET Framework 4.7.2 | .NET Framework 4.7.2 | 10000000 |  64,975,371.5 μs |   803,709.49 μs |    712,467.53 μs | 3981000.0000 |  842000.0000 | 11000.0000 | 25,936,793,336 B |
|          ParallelFunctionalQuickSort | .NET Framework 4.7.2 | .NET Framework 4.7.2 | 10000000 | 122,799,627.0 μs | 2,024,817.07 μs |  1,894,015.14 μs | 5626000.0000 |  938000.0000 |  7000.0000 | 34,816,886,744 B |
| OptimizedParallelFunctionalQuickSort | .NET Framework 4.7.2 | .NET Framework 4.7.2 | 10000000 |  89,672,039.9 μs | 6,002,839.60 μs | 17,699,508.00 μs | 4592000.0000 | 1080000.0000 |  6000.0000 | 29,965,293,376 B |
|        SequentialImperativeQuickSort | .NET Framework 4.7.2 | .NET Framework 4.7.2 | 10000000 |   2,225,040.1 μs |     4,275.01 μs |      3,569.82 μs |   78000.0000 |   27000.0000 |  1000.0000 |    521,420,752 B |
|          ParallelImperativeQuickSort | .NET Framework 4.7.2 | .NET Framework 4.7.2 | 10000000 |  50,735,922.3 μs |   248,177.87 μs |    232,145.73 μs | 4960000.0000 |   33000.0000 |  1000.0000 |  8,627,295,888 B |
| OptimizedParallelImperativeQuickSort | .NET Framework 4.7.2 | .NET Framework 4.7.2 | 10000000 |   2,456,713.4 μs |    41,301.48 μs |     70,133.06 μs |  600000.0000 |   29000.0000 |  1000.0000 |  1,389,444,552 B |

Benchmarks with issues:
  Net47QuickSortBenchmark.SequentialFunctionalQuickSort: .NET Core 3.1(Runtime=.NET Core 3.1) [N=10000]
  Net47QuickSortBenchmark.ParallelFunctionalQuickSort: .NET Core 3.1(Runtime=.NET Core 3.1) [N=10000]
  Net47QuickSortBenchmark.OptimizedParallelFunctionalQuickSort: .NET Core 3.1(Runtime=.NET Core 3.1) [N=10000]
  Net47QuickSortBenchmark.SequentialImperativeQuickSort: .NET Core 3.1(Runtime=.NET Core 3.1) [N=10000]
  Net47QuickSortBenchmark.ParallelImperativeQuickSort: .NET Core 3.1(Runtime=.NET Core 3.1) [N=10000]
  Net47QuickSortBenchmark.OptimizedParallelImperativeQuickSort: .NET Core 3.1(Runtime=.NET Core 3.1) [N=10000]
  Net47QuickSortBenchmark.SequentialFunctionalQuickSort: .NET Core 3.1(Runtime=.NET Core 3.1) [N=100000]
  Net47QuickSortBenchmark.ParallelFunctionalQuickSort: .NET Core 3.1(Runtime=.NET Core 3.1) [N=100000]
  Net47QuickSortBenchmark.OptimizedParallelFunctionalQuickSort: .NET Core 3.1(Runtime=.NET Core 3.1) [N=100000]
  Net47QuickSortBenchmark.SequentialImperativeQuickSort: .NET Core 3.1(Runtime=.NET Core 3.1) [N=100000]
  Net47QuickSortBenchmark.ParallelImperativeQuickSort: .NET Core 3.1(Runtime=.NET Core 3.1) [N=100000]
  Net47QuickSortBenchmark.OptimizedParallelImperativeQuickSort: .NET Core 3.1(Runtime=.NET Core 3.1) [N=100000]
  Net47QuickSortBenchmark.SequentialFunctionalQuickSort: .NET Core 3.1(Runtime=.NET Core 3.1) [N=1000000]
  Net47QuickSortBenchmark.ParallelFunctionalQuickSort: .NET Core 3.1(Runtime=.NET Core 3.1) [N=1000000]
  Net47QuickSortBenchmark.OptimizedParallelFunctionalQuickSort: .NET Core 3.1(Runtime=.NET Core 3.1) [N=1000000]
  Net47QuickSortBenchmark.SequentialImperativeQuickSort: .NET Core 3.1(Runtime=.NET Core 3.1) [N=1000000]
  Net47QuickSortBenchmark.ParallelImperativeQuickSort: .NET Core 3.1(Runtime=.NET Core 3.1) [N=1000000]
  Net47QuickSortBenchmark.OptimizedParallelImperativeQuickSort: .NET Core 3.1(Runtime=.NET Core 3.1) [N=1000000]
  Net47QuickSortBenchmark.SequentialFunctionalQuickSort: .NET Core 3.1(Runtime=.NET Core 3.1) [N=10000000]
  Net47QuickSortBenchmark.ParallelFunctionalQuickSort: .NET Core 3.1(Runtime=.NET Core 3.1) [N=10000000]
  Net47QuickSortBenchmark.OptimizedParallelFunctionalQuickSort: .NET Core 3.1(Runtime=.NET Core 3.1) [N=10000000]
  Net47QuickSortBenchmark.SequentialImperativeQuickSort: .NET Core 3.1(Runtime=.NET Core 3.1) [N=10000000]
  Net47QuickSortBenchmark.ParallelImperativeQuickSort: .NET Core 3.1(Runtime=.NET Core 3.1) [N=10000000]
  Net47QuickSortBenchmark.OptimizedParallelImperativeQuickSort: .NET Core 3.1(Runtime=.NET Core 3.1) [N=10000000]
