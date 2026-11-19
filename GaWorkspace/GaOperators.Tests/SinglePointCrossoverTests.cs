using GaOperators;
using Xunit;

namespace GaOperators.Tests;

public class SinglePointCrossoverTests
{
    [Fact]
    public void ProbabilityZero_ReturnsClones()
    {
        var parent1 = new Chromosome(new[] { true, false, true, false })
        {
            Fitness = 1
        };
        var parent2 = new Chromosome(new[] { false, true, false, true })
        {
            Fitness = 2
        };

        var crossover = new SinglePointCrossover(0);
        var (child1, child2) = crossover.Cross(parent1, parent2, new Random(1));

        Assert.NotSame(parent1, child1);
        Assert.NotSame(parent2, child2);
        Assert.Equal(parent1.Genes, child1.Genes);
        Assert.Equal(parent2.Genes, child2.Genes);
        Assert.Equal(parent1.Fitness, child1.Fitness);
        Assert.Equal(parent2.Fitness, child2.Fitness);
    }

    [Fact]
    public void ProbabilityOne_ReturnsChildrenWithSameLengthAndDoesNotChangeParents()
    {
        var parent1 = new Chromosome(new[] { true, false, true, false });
        var parent2 = new Chromosome(new[] { false, true, false, true });

        var before1 = parent1.ToArray();
        var before2 = parent2.ToArray();

        var crossover = new SinglePointCrossover(1);
        var (child1, child2) = crossover.Cross(parent1, parent2, new Random(123));

        Assert.Equal(parent1.Length, child1.Length);
        Assert.Equal(parent2.Length, child2.Length);
        Assert.Equal(before1, parent1.ToArray());
        Assert.Equal(before2, parent2.ToArray());
    }

    [Fact]
    public void DifferentParentLengths_Throws()
    {
        var parent1 = new Chromosome(new[] { true, false });
        var parent2 = new Chromosome(new[] { true, false, true });

        var crossover = new SinglePointCrossover(1);

        Assert.Throws<ArgumentException>(
            () => crossover.Cross(parent1, parent2, new Random(1)));
    }

    [Fact]
    public void LengthOne_ReturnsClonesEvenWithProbabilityOne()
    {
        var parent1 = new Chromosome(new[] { true }) { Fitness = 1 };
        var parent2 = new Chromosome(new[] { false }) { Fitness = 2 };

        var crossover = new SinglePointCrossover(1);
        var (child1, child2) = crossover.Cross(parent1, parent2, new Random(1));

        Assert.NotSame(parent1, child1);
        Assert.NotSame(parent2, child2);
        Assert.Equal(parent1.Genes, child1.Genes);
        Assert.Equal(parent2.Genes, child2.Genes);
    }
}