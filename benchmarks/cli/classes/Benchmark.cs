namespace EGP.ProjectBoost.Benchmarks;

using Xunit;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

public partial class Benchmark1
{
#if (!DEBUG)
	[Fact]
	public void Benchmark1_Benchmark()
	{
		BenchmarkRunner.Run<Benchmark1Benchmark>();
	}
#endif
}

public partial class Benchmark1Benchmark : BenchmarkContext
{
	[Benchmark]
	public void Benchmark1()
	{
		System.Threading.Thread.Sleep(1); // sleep for 1 second
	}
}
