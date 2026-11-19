using GaOperators;
using Xunit;

namespace GaOperators.Tests;

public class BitFlipMutationTests
{
    [Fact]
    public void ProbabilityZero_ReturnsNewChromosomeWithSameGenes()
    {
        var source = new Chromosome(new[] { true, false, true })
        {
            Fitness = 2.0
        };

        var mutation = new BitFlipMutation(0);
        var result = mutation.Mutate(source, new Random(1));

        Assert.NotSame(source, result);
        Assert.Equal(source.Genes, result.Genes);
        Assert.Equal(source.Fitness, result.Fitness);
    }

    [Fact]
    public void ProbabilityOne_InvertsAllGenes()
    {
        var source = new Chromosome(new[] { true, false, true });
        var mutation = new BitFlipMutation(1);

        var result = mutation.Mutate(source, new Random(1));

        Assert.Equal(new[] { false, true, false }, result.ToArray());
    }

    [Fact]
    public void Mutate_DoesNotChangeSource()
    {
        var source = new Chromosome(new[] { true, false, true });
        var before = source.ToArray();

        var mutation = new BitFlipMutation(1);
        mutation.Mutate(source, new Random(1));

        Assert.Equal(before, source.ToArray());
    }

    [Theory]
    [InlineData(-0.1)]
    [InlineData(1.1)]
    [InlineData(double.NaN)]
    public void Constructor_RejectsInvalidProbability(double probability)
    {
        Assert.Throws<ArgumentOutOfRangeException>(
            () => new BitFlipMutation(probability));
    }
}