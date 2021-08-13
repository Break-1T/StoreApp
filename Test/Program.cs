using BenchmarkDotNet.Running;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            BenchmarkRunner.Run<EntityTest>();
        }
    }
}
