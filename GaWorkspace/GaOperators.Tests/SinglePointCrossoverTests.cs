using GaOperators;
using Xunit;

namespace GaOperators.Tests;

public class SinglePointCrossoverTests
{
    private static readonly bool[] P1 = { true, false, true, false, true, false };
    private static readonly bool[] P2 = { false, true, false, true, false, true };

    [Fact]
    public void ProbabilityZero_ReturnsClones()
    {
        var p1 = new Chromosome(P1);
        var p2 = new Chromosome(P2);
        var (c1, c2) = new SinglePointCrossover(0).Cross(p1, p2, new Random(1));

        Assert.NotSame(p1, c1);
        Assert.NotSame(p2, c2);
        Assert.Equal(P1, c1.ToArray());
        Assert.Equal(P2, c2.ToArray());
    }

    [Fact]
    public void ProbabilityOne_LengthsPreserved()
    {
        var p1 = new Chromosome(P1);
        var p2 = new Chromosome(P2);
        var (c1, c2) = new SinglePointCrossover(1).Cross(p1, p2, new Random(1));

        Assert.Equal(P1.Length, c1.Length);
        Assert.Equal(P2.Length, c2.Length);
    }

    [Fact]
    public void Children_ArePrefixSuffixCombinations()
    {
        var p1 = new Chromosome(P1);
        var p2 = new Chromosome(P2);
        var crossover = new SinglePointCrossover(1);

        for (int seed = 0; seed < 30; seed++)
        {
            var (c1, c2) = crossover.Cross(p1, p2, new Random(seed));
            Assert.True(IsSinglePointChild(c1.ToArray(), P1, P2));
            Assert.True(IsSinglePointChild(c2.ToArray(), P2, P1));
        }
    }

    [Fact]
    public void Parents_NotChanged()
    {
        var p1 = new Chromosome(P1);
        var p2 = new Chromosome(P2);
        new SinglePointCrossover(1).Cross(p1, p2, new Random(123));

        Assert.Equal(P1, p1.ToArray());
        Assert.Equal(P2, p2.ToArray());
    }

    [Fact]
    public void DifferentLengths_Throws()
    {
        var p1 = new Chromosome(new[] { true, false });
        var p2 = new Chromosome(new[] { true, false, true });
        Assert.Throws<ArgumentException>(
            () => new SinglePointCrossover(1).Cross(p1, p2, new Random(1)));
    }

    [Fact]
    public void LengthOne_ReturnsClones()
    {
        var p1 = new Chromosome(new[] { true });
        var p2 = new Chromosome(new[] { false });
        var (c1, c2) = new SinglePointCrossover(1).Cross(p1, p2, new Random(1));

        Assert.Equal(new[] { true }, c1.ToArray());
        Assert.Equal(new[] { false }, c2.ToArray());
    }

    [Fact]
    public void NullParents_Throw()
    {
        var p1 = new Chromosome(P1);
        var crossover = new SinglePointCrossover(1);

        Assert.Throws<ArgumentNullException>(
            () => crossover.Cross(null!, p1, new Random(1)));
        Assert.Throws<ArgumentNullException>(
            () => crossover.Cross(p1, null!, new Random(1)));
        Assert.Throws<ArgumentNullException>(
            () => crossover.Cross(p1, p1, null!));
    }

    [Theory]
    [InlineData(-0.01)]
    [InlineData(1.01)]
    [InlineData(double.NaN)]
    public void InvalidProbability_Throws(double p)
        => Assert.Throws<ArgumentOutOfRangeException>(() => new SinglePointCrossover(p));

    private static bool IsSinglePointChild(bool[] child, bool[] a, bool[] b)
    {
        for (int p = 1; p < child.Length; p++)
        {
            bool ok = true;
            for (int i = 0; i < p && ok; i++) ok &= child[i] == a[i];
            for (int i = p; i < child.Length && ok; i++) ok &= child[i] == b[i];
            if (ok) return true;
        }
        return false;
    }
}