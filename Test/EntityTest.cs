using System;
using BenchmarkDotNet.Attributes;

namespace Test
{
    [RankColumn,MemoryDiagnoser]
    class EntityTest
    {
        [Benchmark]
        public void ShowWindow()
        {
        }
    }
}
