using System;
using System.Reflection;
using BenchmarkDotNet.Running;

namespace Dotnetos.AsyncExpert.Homework.Module01.Benchmark
{
    class Program
    {
        static void Main(string[] args)
        {
            BenchmarkSwitcher.FromAssembly(Assembly.GetExecutingAssembly()).Run(args);
            // const ulong n = 15ul;
            // var fibonacciCalc = new FibonacciCalc();
            // Console.WriteLine(fibonacciCalc.Iterative(n));
            // Console.WriteLine(fibonacciCalc.Recursive(n));
            // Console.WriteLine(fibonacciCalc.RecursiveWithMemoization(n));
        }
    }
}
