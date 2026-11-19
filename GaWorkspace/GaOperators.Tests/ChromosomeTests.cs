using GaOperators;
using Xunit;

namespace GaOperators.Tests;

public class ChromosomeTests
{
    [Fact]
    public void Constructor_CopiesInputArray()
    {
        var genes = new[] { true, false, true };
        var chromosome = new Chromosome(genes);

        genes[0] = false;
        genes[1] = true;

        Assert.True(chromosome[0]);
        Assert.False(chromosome[1]);
        Assert.True(chromosome[2]);
    }

    [Fact]
    public void Clone_ReturnsIndependentObjectWithSameGenesAndFitness()
    {
        var original = new Chromosome(new[] { true, false, true })
        {
            Fitness = 3.5
        };

        var clone = original.Clone();

        Assert.NotSame(original, clone);
        Assert.Equal(original.Fitness, clone.Fitness);
        Assert.Equal(original.Genes, clone.Genes);

        var cloneGenes = clone.ToArray();
        cloneGenes[0] = false;

        Assert.True(original[0]);
    }

    [Fact]
    public void Constructor_RejectsNullAndEmptyArray()
    {
        Assert.Throws<ArgumentNullException>(
            () => new Chromosome(null!));

        Assert.Throws<ArgumentException>(
            () => new Chromosome(Array.Empty<bool>()));
    }
}