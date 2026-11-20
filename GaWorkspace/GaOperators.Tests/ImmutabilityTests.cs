using GaOperators;
using Xunit;

namespace GaOperators.Tests;

public class ImmutabilityTests
{
    [Fact]
    public void AllOperators_DoNotMutateInputs()
    {
        var genes1 = new[] { true, false, true, false };
        var genes2 = new[] { false, true, false, true };
        var original1 = (bool[])genes1.Clone();
        var original2 = (bool[])genes2.Clone();

        for (int seed = 0; seed < 25; seed++)
        {
            var rng = new Random(seed);
            var p1 = new Chromosome(genes1) { Fitness = 1 };
            var p2 = new Chromosome(genes2) { Fitness = 2 };

            new SinglePointCrossover(0.7).Cross(p1, p2, rng);
            new UniformCrossover(0.7).Cross(p1, p2, rng);
            new BitFlipMutation(0.5).Mutate(p1, rng);

            Assert.Equal(original1, genes1);
            Assert.Equal(original2, genes2);
            Assert.Equal(original1, p1.ToArray());
            Assert.Equal(original2, p2.ToArray());
        }
    }
}