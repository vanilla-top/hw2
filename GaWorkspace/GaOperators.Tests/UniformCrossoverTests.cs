using GaOperators;
using Xunit;

namespace GaOperators.Tests;

public class UniformCrossoverTests
{
    private static readonly bool[] P1 = { true, false, true, false, true, false };
    private static readonly bool[] P2 = { false, true, false, true, false, true };

    [Fact]
    public void ProbabilityZero_ReturnsClones()
    {
        var p1 = new Chromosome(P1);
        var p2 = new Chromosome(P2);
        var (c1, c2) = new UniformCrossover(0).Cross(p1, p2, new Random(1));

        Assert.NotSame(p1, c1);
        Assert.NotSame(p2, c2);
        Assert.Equal(P1, c1.ToArray());
        Assert.Equal(P2, c2.ToArray());
    }

    [Fact]
    public void ProbabilityOne_LengthsMatch()
    {
        var p1 = new Chromosome(P1);
        var p2 = new Chromosome(P2);
        var (c1, c2) = new UniformCrossover(1).Cross(p1, p2, new Random(1));

        Assert.Equal(P1.Length, c1.Length);
        Assert.Equal(P2.Length, c2.Length);
    }

    [Fact]
    public void EachGene_ComesFromParentsOnSamePosition()
    {
        var p1 = new Chromosome(P1);
        var p2 = new Chromosome(P2);
        var crossover = new UniformCrossover(1);

        for (int seed = 0; seed < 50; seed++)
        {
            var (c1, c2) = crossover.Cross(p1, p2, new Random(seed));

            for (int i = 0; i < P1.Length; i++)
            {
                Assert.Equal(
                    new[] { P1[i], P2[i] }.OrderBy(x => x).ToArray(),
                    new[] { c1[i], c2[i] }.OrderBy(x => x).ToArray());
            }
        }
    }

    [Fact]
    public void Parents_NotChanged()
    {
        var p1 = new Chromosome(P1);
        var p2 = new Chromosome(P2);
        new UniformCrossover(1).Cross(p1, p2, new Random(7));

        Assert.Equal(P1, p1.ToArray());
        Assert.Equal(P2, p2.ToArray());
    }

    [Fact]
    public void DifferentLengths_Throws()
    {
        var p1 = new Chromosome(new[] { true, false });
        var p2 = new Chromosome(new[] { true, false, true });
        Assert.Throws<ArgumentException>(
            () => new UniformCrossover(1).Cross(p1, p2, new Random(1)));
    }

    [Theory]
    [InlineData(-0.01)]
    [InlineData(1.01)]
    [InlineData(double.NaN)]
    public void InvalidProbability_Throws(double p)
        => Assert.Throws<ArgumentOutOfRangeException>(() => new UniformCrossover(p));
}