﻿using System.Collections.Generic;
using BenchmarkDotNet.Attributes;

namespace Dotnetos.AsyncExpert.Homework.Module01.Benchmark
{
    [DisassemblyDiagnoser(exportCombinedDisassemblyReport: true)]
    [MemoryDiagnoser]
    public class FibonacciCalc
    {
        private readonly IDictionary<ulong, ulong> _fibs = new Dictionary<ulong, ulong> { { 1, 1 }, { 2, 1 } };

        // HOMEWORK:
        // 1. Write implementations for RecursiveWithMemoization and Iterative solutions
        // 2. Add MemoryDiagnoser to the benchmark
        // 3. Run with release configuration and compare results
        // 4. Open disassembler report and compare machine code
        // 
        // You can use the discussion panel to compare your results with other students

        [Benchmark(Baseline = true)]
        [ArgumentsSource(nameof(Data))]
        public ulong Recursive(ulong n)
        {
            if (n == 1 || n == 2) return 1;
            return Recursive(n - 2) + Recursive(n - 1);
        }

        [Benchmark]
        [ArgumentsSource(nameof(Data))]
        public ulong RecursiveWithMemoization(ulong n)
        {
            if (!_fibs.ContainsKey(n))
            {
                _fibs[n] = RecursiveWithMemoization(n - 1) + RecursiveWithMemoization(n - 2);
            }

            return _fibs[n];
        }

        [Benchmark]
        [ArgumentsSource(nameof(Data))]
        public ulong Iterative(ulong n)
        {
            if (n == 1 || n == 2) return 1;
            ulong prev = 1;
            ulong prePrev = 1;
            ulong fib = 0;
            while (n > 2)
            {
                fib = prev + prePrev;
                prePrev = prev;
                prev = fib;
                n--;
            }

            return fib;
        }

        public IEnumerable<ulong> Data()
        {
            yield return 15;
            yield return 35;
        }
    }
}